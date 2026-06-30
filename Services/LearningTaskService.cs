using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Data;
using TraineeManagement.DTOs;
using TraineeManagement.Models;
using TraineeManagement.Extensions;
using TraineeManagement.Exceptions;

namespace TraineeManagement.Services;

public class LearningTaskService : ILearningTaskService
{
    private readonly AppDbContext _context;
    private readonly ILogger<LearningTask> _logger;

    public LearningTaskService(AppDbContext context, ILogger<LearningTask> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<PagedResponse<LearningTaskResponse>> GetAll(LearningTaskSearchParameters learningTaskSearchParameters)
    {
        IQueryable<LearningTask> query = _context.LearningTasks.AsQueryable();
        string search = learningTaskSearchParameters.Search!;
        string status = learningTaskSearchParameters.Status!;

        if(!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(lt => lt.Title.ToLower().Contains(search) ||
            lt.Description.ToLower().Contains(search) ||
            lt.ExpectedTechStack.ToLower().Contains(search)
            );
        }

        if(!string.IsNullOrWhiteSpace(status))
            query = query.Where(t => t.Status == status);

        int totalRecords = await query.CountAsync();

        List<LearningTaskResponse> pagedData = await query.Skip((learningTaskSearchParameters.PageNumber - 1) * learningTaskSearchParameters.PageSize)
        .Take(learningTaskSearchParameters.PageSize)
        .Select(lt => new LearningTaskResponse(lt))
        .ToListAsync();
        

        return new PagedResponse<LearningTaskResponse>(pagedData, totalRecords, learningTaskSearchParameters.PageNumber, learningTaskSearchParameters.PageSize);
    }

    public async Task<LearningTaskResponse?> GetById(int Id)
    {
        LearningTask? learningTask = await _context.LearningTasks.FindAsync(Id);
        if(learningTask == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Learning Task", Id);
            return null;
        }
        return new LearningTaskResponse(learningTask);
    }

    public async Task<LearningTaskResponse> Create(CreateLearningTaskRequest createLearningTaskRequest)
    {
        if(createLearningTaskRequest.DueDate < DateTime.UtcNow)
        {
            throw new BadRequestException("Due Date should be greater than current date");
        }
        LearningTask learningTask = new LearningTask(createLearningTaskRequest);
        await _context.LearningTasks.AddAsync(learningTask);
        await _context.SaveChangesAsync();
        return new LearningTaskResponse(learningTask);
    }

    public async Task<LearningTaskResponse?> Update(int Id, UpdateLearningTask updateLearningTask)
    {
        LearningTask? learningTask = await _context.LearningTasks.FindAsync(Id);
        if(learningTask == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Learning Task", Id);
            return null;
        }

        
        if(updateLearningTask.DueDate < DateTime.UtcNow)
        {
            throw new BadRequestException("Due Date should be greater than current date");
        }
        learningTask.Title = updateLearningTask.Title;
        learningTask.Description = updateLearningTask.Description;
        learningTask.ExpectedTechStack = updateLearningTask.ExpectedTechStack;
        learningTask.DueDate = updateLearningTask.DueDate.ToUtcSecondPrecision();
        learningTask.Status = updateLearningTask.Status;
        learningTask.UpdatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
        await _context.SaveChangesAsync();
        _logger.LogInformation("LearningTask event: {ActionEvent} occurred for LearningTaskId: {MentorId}", "Updated", learningTask.Id);
        return new LearningTaskResponse(learningTask);
    }

    public async Task<bool> Delete(int Id)
    {
        LearningTask? learningTask = await _context.LearningTasks.FindAsync(Id);

        if(learningTask == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Learning Task", Id);
            return false;
        }

        _context.LearningTasks.Remove(learningTask);
        await _context.SaveChangesAsync();
        _logger.LogInformation("LearningTask event: {ActionEvent} occurred for Id: {LearningTaskId}", "Deleted", Id);
        return true;
    }
}