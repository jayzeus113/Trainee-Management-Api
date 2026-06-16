using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TraineeManagement.Exceptions;
using Mysqlx;

namespace TraineeManagement.Services;
public class TraineeService : ITraineeService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TraineeService> _logger;

    public TraineeService(AppDbContext context, ILogger<TraineeService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PagedResponse<TraineeResponse>> GetAll(TraineeSearchParameters traineeSearchParameters)
    {
        IQueryable<Trainee> query = _context.Trainees.AsQueryable();
        string search = traineeSearchParameters.Search!;
        string status = traineeSearchParameters.Status!;

        if(!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.FirstName.ToLower().Contains(search) ||
            t.LastName.ToLower().Contains(search) ||
            t.Email.ToLower().Contains(search) ||
            t.TechStack.ToLower().Contains(search)
            );
        }

        if(!string.IsNullOrWhiteSpace(status))
            query = query.Where(t => t.Status == status);

        int totalRecords = await query.CountAsync();

        List<TraineeResponse> pagedData = await query.Skip((traineeSearchParameters.PageNumber - 1) * traineeSearchParameters.PageSize)
        .Take(traineeSearchParameters.PageSize)
        .Select(t => new TraineeResponse(t))
        .ToListAsync();
        

        return new PagedResponse<TraineeResponse>(pagedData, totalRecords, traineeSearchParameters.PageNumber, traineeSearchParameters.PageSize);
    }
    public async Task<TraineeResponse> GetById(int Id)
    {
        Trainee? trainee = await _context.Trainees.FindAsync(Id);
        if(trainee == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Trainee", Id);
            throw new NotFoundException($"Trainee not found with Id: {Id}");
        }
        return new TraineeResponse(trainee);
    }

    public async Task<TraineeResponse> Create(CreateTraineeRequest createTraineeRequest)
    {
        Trainee trainee = new Trainee(createTraineeRequest);
        await _context.Trainees.AddAsync(trainee);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Trainee event: {ActionEvent} occured for {TraineeId}", "Created", trainee.Id);
        return new TraineeResponse(trainee);
    }

    public async Task<TraineeResponse> Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        Trainee? trainee = await _context.Trainees.FindAsync(Id);
        if(trainee == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Trainee", Id);
            throw new NotFoundException($"Trainee not found with Id: {Id}");
        }
        trainee.FirstName = updateTraineeRequest.FirstName;
        trainee.LastName = updateTraineeRequest.LastName;
        trainee.Email = updateTraineeRequest.Email;
        trainee.TechStack = updateTraineeRequest.TechStack;
        trainee.Status = updateTraineeRequest.Status;
        DateTime dt = DateTime.Now;
        dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        trainee.UpdatedDate = dt;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Trainee event: {ActionEvent} occurred for TraineeId: {TraineeId}", "Updated", trainee.Id);
        return new TraineeResponse(trainee);
    }

    public async Task<bool> Delete(int Id)
    {
        Trainee? trainee = await _context.Trainees.FindAsync(Id);
        if(trainee == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Trainee", Id);
            throw new NotFoundException($"Trainee not found with Id: {Id}");
        }
        _context.Trainees.Remove(trainee);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Trainee event: {ActionEvent} occurred for TraineeId: {TraineeId}", "Updated", Id);
        return true;
    }
}