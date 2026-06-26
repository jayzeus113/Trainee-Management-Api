using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Exceptions;

namespace TraineeManagement.Services;
public class ProcessingJobService : IProcessingJobService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProcessingJobService> _logger;

    public ProcessingJobService(AppDbContext context, ILogger<ProcessingJobService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProcessingJobResponse> GetById(int Id)
    {
        ProcessingJob? processingJob = await _context.ProcessingJobs.FindAsync(Id);
        if(processingJob == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "ProcessingJob", Id);
            throw new NotFoundException($"ProcessingJob not found with Id: {Id}");
        }
        return new ProcessingJobResponse(processingJob);
    }
}