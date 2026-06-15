using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Exceptions;

namespace TraineeManagement.Services;
public class TaskAssignmentService : ITaskAssignmentService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TaskAssignmentService> _logger;

    public TaskAssignmentService(AppDbContext context, ILogger<TaskAssignmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<TaskAssignmentResponse>> GetAll()
    {
        return await _context.TaskAssignments.Select(ta => new TaskAssignmentResponse(ta)).ToListAsync();
    }
    public async Task<TaskAssignmentResponse> GetById(int Id)
    {
        TaskAssignment? taskAssignment = await _context.TaskAssignments.FindAsync(Id);
        if(taskAssignment == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Task Assignment", Id);
            throw new NotFoundException($"Task Assignment not found with Id: {Id}");
        }
        return new TaskAssignmentResponse(taskAssignment);
    }

    public async Task<TaskAssignmentResponse> Create(CreateTaskAssignmentRequest createTaskAssignmentRequest)
    {
        bool traineeExists = await _context.Trainees.AnyAsync(t => t.Id == createTaskAssignmentRequest.TraineeId);

        if(!traineeExists)
        throw new NotFoundException($"Trainee not found with Id: {createTaskAssignmentRequest.TraineeId}");

        bool mentorExists = await _context.Mentors.AnyAsync(m => m.Id == createTaskAssignmentRequest.MentorId);

        if(!mentorExists)
        throw new NotFoundException($"Mentor not found with Id: {createTaskAssignmentRequest.MentorId}");

        bool learningTaskExists = await _context.LearningTasks.AnyAsync(lt => lt.Id == createTaskAssignmentRequest.LearningTaskId);
        
        if(!learningTaskExists)
        throw new NotFoundException($"Learning Task not found with Id: {createTaskAssignmentRequest.LearningTaskId}");

        if(createTaskAssignmentRequest.DueDate < createTaskAssignmentRequest.AssigenedDate) throw new BadRequestException("DueDate should be after AssignedDate");

        TaskAssignment taskAssignment = new TaskAssignment(createTaskAssignmentRequest);
        await _context.TaskAssignments.AddAsync(taskAssignment);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Task Assignment event: {ActionEvent} occured for {TaskAssignmentId}", "Created", taskAssignment.Id);
        return new TaskAssignmentResponse(taskAssignment);
    }

    public async Task<TaskAssignmentResponse> Update(int Id, UpdateTaskAssignmentRequest updateTaskAssignmentRequest)
    {
        TaskAssignment? taskAssignment = await _context.TaskAssignments.FindAsync(Id);
        if(taskAssignment == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Trainee", Id);
            throw new NotFoundException($"Task Assignment not found with Id: {Id}");
        }
        taskAssignment.Status = updateTaskAssignmentRequest.Status;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Task Assignment event: {ActionEvent} occurred for TaskAssignmentId: {TaskAssignmentId}", "Updated", taskAssignment.Id);
        return new TaskAssignmentResponse(taskAssignment);
    }
}