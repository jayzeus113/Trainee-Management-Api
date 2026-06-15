using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Exceptions;

namespace TraineeManagement.Services;
public class SubmissionService : ISubmissionService
{
    private readonly AppDbContext _context;
    private readonly ILogger<SubmissionService> _logger;

    public SubmissionService(AppDbContext context, ILogger<SubmissionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<SubmissionResponse>> GetAll()
    {
        return await _context.Submissions.Select(ta => new SubmissionResponse(ta)).ToListAsync();
    }
    public async Task<SubmissionResponse> GetById(int Id)
    {
        Submission? submission = await _context.Submissions.FindAsync(Id);
        if(submission == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Submission", Id);
            throw new NotFoundException($"Submission not found with Id: {Id}");
        }
        return new SubmissionResponse(submission);
    }

    public async Task<SubmissionResponse> Create(CreateSubmissionRequest createSubmissionRequest)
    {
        bool taskAssignmentExists = await _context.TaskAssignments.AnyAsync(ta => ta.Id == createSubmissionRequest.TaskAssignmentId);

        if(!taskAssignmentExists)
        throw new NotFoundException($"Task Assignment not found with Id: {createSubmissionRequest.TaskAssignmentId}");

        Submission submission = new Submission(createSubmissionRequest);
        await _context.Submissions.AddAsync(submission);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Submission event: {ActionEvent} occured for {SubmissionId}", "Created", submission.Id);
        return new SubmissionResponse(submission);
    }
}