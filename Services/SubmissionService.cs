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
    private readonly RedisCacheSercvice _redisCacheSercvice;

 
 
    public SubmissionService(AppDbContext context, ILogger<SubmissionService> logger, IFileStorageService fileStorageService, RedisCacheSercvice redisCacheSercvice)
    {
        _logger = logger;
        _context = context;
        _fileStorageService = fileStorageService;
        _redisCacheSercvice = redisCacheSercvice;
    }
   
 
    public async Task<List<SubmissionResponse>> GetAll()
    {
        return await _context.Submissions.Select(t => new SubmissionResponse(t)).ToListAsync();
    }
 
    public async Task<SubmissionResponse> GetById(int Id)
    {
        string cacheKey = $"submission:{Id}";

        SubmissionResponse? cachedSubmissionResponse = await _redisCacheSercvice.GetKeyAsync<SubmissionResponse>(cacheKey);

        if(cachedSubmissionResponse != default) {
            _logger.LogInformation("Cache hit, Found the Submission with Id: {Id}", Id);
            return cachedSubmissionResponse;
        }

        Submission? submission = await _context.Submissions.FindAsync(Id);
        if(submission == null)
        {
            _logger.LogInformation("Submission not found with {Id}", Id);
            throw new NotFoundException($"Submission not found with Id: {Id}");
        }
        SubmissionResponse submissionResponse = new SubmissionResponse(submission);
        await _redisCacheSercvice.SetKeyAsync(cacheKey, submissionResponse);
        return submissionResponse;
    }
 
    public async Task<SubmissionResponse> Create(CreateSubmissionRequest createSubmissionRequest)
    {
        bool taskAssignmentExists = await _context.TaskAssignments.AnyAsync(t => t.Id == createSubmissionRequest.TaskAssignmentId);
        if(!taskAssignmentExists) throw new NotFoundException($"TaskAssignment not found with Id: {createSubmissionRequest.TaskAssignmentId}");
 
        Submission submission = new Submission(createSubmissionRequest);
        await _context.Submissions.AddAsync(submission);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Successfully Created Submission: {Id} ", submission.Id);
        return new SubmissionResponse(submission);
    }
 
    public async Task<SubmissionFileResponse> UploadFile(int userId, int submissionId, CreateSubmissionFileRequest createSubmissionFileRequest)
    {
        bool submission = await _context.Submissions.AnyAsync(s => s.Id == submissionId);
        if(!submission) throw new NotFoundException($"TaskAssignment not found with Id: {submissionId}");
 
        IFormFile file = createSubmissionFileRequest.File;
        string generatedFileName = _fileStorageService.GenerateUniqueFileName(file.FileName);
        string contentType = _fileStorageService.GetContentType(generatedFileName);
        string? userName = await _context.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstOrDefaultAsync();
        await _fileStorageService.SaveAsync(generatedFileName, file.OpenReadStream());
        string CheckSum = _fileStorageService.GetChecksum(generatedFileName);
        SubmissionFile submissionFile = new SubmissionFile
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
        SubmissionFile? submissionFile = await _context.SubmissionFiles.FindAsync(submissionFileId) ?? throw new NotFoundException($"SubmissionFile not found with Id: {submissionFileId}");
        return await _fileStorageService.OpenReadAsync(submissionFile.GeneratedStorageName);
    }

    public async Task<bool> DeleteFile(int submissionFileId)
    {
        SubmissionFile? submissionFile = await _context.SubmissionFiles.FindAsync(submissionFileId) ?? throw new NotFoundException($"SubmissionFile not found with Id: {submissionFileId}");
        await _fileStorageService.DeleteAsync(submissionFile.GeneratedStorageName);
        _context.Remove(submissionFile);
        await _context.SaveChangesAsync();
        return true;
    }
}
 