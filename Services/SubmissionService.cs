using TraineeManagement.Models;
using TraineeManagement.DTOs;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Data;
using TraineeManagement.Exceptions;
using TraineeManagement.Extensions;

namespace TraineeManagement.Services;
 
public class SubmissionService : ISubmissionService
{
    private readonly AppDbContext _context;
    private readonly ILogger<SubmissionService> _logger;
    private readonly IFileStorageService _fileStorageService;
    private readonly RedisCacheService _redisCacheService;
    private readonly IMessagePublisher _messagePublisher;

 
    public SubmissionService(AppDbContext context, ILogger<SubmissionService> logger, IFileStorageService fileStorageService, RedisCacheService redisCacheService, IMessagePublisher messagePublisher)
    {
        _logger = logger;
        _context = context;
        _fileStorageService = fileStorageService;
        _redisCacheService = redisCacheService;
        _messagePublisher = messagePublisher;
    }
   
 
    public async Task<List<SubmissionResponse>> GetAll()
    {
        _logger.LogDebug("Retrieving all submissions from database.");
        return await _context.Submissions.Select(t => new SubmissionResponse(t)).ToListAsync();
    }
 
    public async Task<SubmissionResponse> GetById(int Id)
    {
        string cacheKey = $"submission:{Id}";
        _logger.LogDebug("Checking Redis cache for key: {CacheKey}", cacheKey);
        SubmissionResponse? cachedSubmissionResponse = await _redisCacheService.GetKeyAsync<SubmissionResponse>(cacheKey);

        if(cachedSubmissionResponse != default) {
            _logger.LogDebug("Cache hit. Found Submission with Id: {SubmissionId}", Id);
            return cachedSubmissionResponse;
        }

        _logger.LogInformation("Cache miss. Fetching Submission from database for Id: {SubmissionId}", Id);
        Submission? submission = await _context.Submissions.FindAsync(Id);
        if(submission == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Submission", Id);
            throw new NotFoundException($"Submission not found with Id: {Id}");
        }
        SubmissionResponse submissionResponse = new SubmissionResponse(submission);

        _logger.LogDebug("Populating Redis cache key: {CacheKey} with Submission data.", cacheKey);
        await _redisCacheService.SetKeyAsync(cacheKey, submissionResponse);
        return submissionResponse;
    }
 
    public async Task<SubmissionResponse> Create(CreateSubmissionRequest createSubmissionRequest)
    {
        bool taskAssignmentExists = await _context.TaskAssignments.AnyAsync(t => t.Id == createSubmissionRequest.TaskAssignmentId);
        if(!taskAssignmentExists)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "TaskAssignment", createSubmissionRequest.TaskAssignmentId);
            throw new NotFoundException($"TaskAssignment not found with Id: {createSubmissionRequest.TaskAssignmentId}");
        }
 
        Submission submission = new Submission(createSubmissionRequest);
        await _context.Submissions.AddAsync(submission);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Submission event: {ActionEvent} occurred for SubmissionId: {SubmissionId}", "Created", submission.Id);
        return new SubmissionResponse(submission);
    }
 
    public async Task<SubmissionFileResponse> UploadFile(int userId, int submissionId, CreateSubmissionFileRequest createSubmissionFileRequest)
    {
        bool submission = await _context.Submissions.AnyAsync(s => s.Id == submissionId);
        if(!submission)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Submission", submissionId);
            throw new NotFoundException($"TaskAssignment not found with Id: {submissionId}");
        }
        
        var correlationId = Guid.NewGuid();
 
        IFormFile file = createSubmissionFileRequest.File;
        string generatedFileName = _fileStorageService.GenerateUniqueFileName(file.FileName);
        string contentType = _fileStorageService.GetContentType(generatedFileName);
        Stream stream = file.OpenReadStream();
        string? userName = await _context.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstOrDefaultAsync();
        await _fileStorageService.SaveAsync(generatedFileName, stream);
        string CheckSum = _fileStorageService.GetChecksum(await _fileStorageService.OpenReadAsync(generatedFileName));
        SubmissionFile submissionFile = new()
        {
            SubmissionId = submissionId,
            OriginalFileName = file.FileName,
            GeneratedStorageName = generatedFileName,
            CheckSum = CheckSum,
            Size = file.Length,
            ContentType = contentType,
            UploadedBy = userId,
            CreatedDate = DateTime.UtcNow.ToUtcSecondPrecision()
        };
        await _context.SubmissionFiles.AddAsync(submissionFile);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Submission file metadata stored. FileId: {FileId}, StoragePathName: {GeneratedName}", submissionFile.Id, generatedFileName);

        try
        {
            var message = new SubmissionProcessingRequested(
                MessageId: Guid.NewGuid(),
                CorrelationId: correlationId,
                SubmissionId: submissionId,
                FileId: submissionFile.Id,
                RequestedAt: DateTime.UtcNow.ToUtcSecondPrecision()
            );

            
            await _messagePublisher.PublishAsync(message);
            ProcessingJob processingJob = new ProcessingJob
            {
                Status = "Queued",
                CorrelationId = correlationId
            };
            await _context.ProcessingJobs.AddAsync(processingJob);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully queued background job validation task. CorrelationId: {CorrelationId}", correlationId);
        } catch(Exception ex)
        {
            _logger.LogError(ex, "Metadata stored successfully, but broker submission dispatch failed. CorrelationId: {CorrelationId}", correlationId);
            throw new ServiceUnavailableException("Database update completed, but background processing pipeline is temporarily offline.");
        }
 
        return new SubmissionFileResponse
        {
            Id = submissionFile.Id,
            SubmissionId = submissionFile.SubmissionId,
            OriginalFileName = submissionFile.OriginalFileName,
            GeneratedStorageName = submissionFile.GeneratedStorageName,
            CheckSum = submissionFile.CheckSum,
            Size = submissionFile.Size,
            ContentType = submissionFile.ContentType,
            UploadedBy = userName!,
            CreatedDate = submissionFile.CreatedDate
        };
    }

    public async Task<FileStream> DownloadFile(int submissionFileId)
    {
        SubmissionFile? submissionFile = await _context.SubmissionFiles.FindAsync(submissionFileId);
        if (submissionFile == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "SubmissionFile", submissionFileId);
            throw new NotFoundException($"SubmissionFile not found with Id: {submissionFileId}");
        }

        _logger.LogInformation("Processing download authorization for FileId: {FileId}", submissionFileId);
        return await _fileStorageService.OpenReadAsync(submissionFile.GeneratedStorageName);
    }

    public async Task<bool> DeleteFile(int submissionFileId)
    {
        SubmissionFile? submissionFile = await _context.SubmissionFiles.FindAsync(submissionFileId);
        if (submissionFile == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "SubmissionFile", submissionFileId);
            throw new NotFoundException($"SubmissionFile not found with Id: {submissionFileId}");
        }
        _logger.LogInformation("Initiating structural file deleting routine. FileId: {FileId}, StoragePath: {GeneratedName}", 
            submissionFileId, submissionFile.GeneratedStorageName);
        await _fileStorageService.DeleteAsync(submissionFile.GeneratedStorageName);
        _context.Remove(submissionFile);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Submission file asset deleted from database and local storage. FileId: {FileId}", submissionFileId);
        return true;
    }
}
 