using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Exceptions;
using TraineeManagement.Extensions;
using Microsoft.Extensions.Logging;

namespace TraineeManagement.Services;

public class MentorService : IMentorService
{
    private readonly AppDbContext _context;
    private readonly ILogger<MentorService> _logger;

    public MentorService(AppDbContext context, ILogger<MentorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PagedResponse<MentorResponse>> GetAll(MentorSearchParameters mentorSearchParameters)
    {
        _logger.LogDebug("Fetching paginated mentors. PageNumber: {PageNumber}, PageSize: {PageSize}, Search: {Search}, Status: {Status}", 
            mentorSearchParameters.PageNumber, mentorSearchParameters.PageSize, mentorSearchParameters.Search, mentorSearchParameters.Status);

        IQueryable<Mentor> query = _context.Mentors.AsQueryable();
        string? search = mentorSearchParameters.Search;
        string? status = mentorSearchParameters.Status;

        if (!string.IsNullOrWhiteSpace(search))
        {

            query = query.Where(t => t.FirstName.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                     t.LastName.Contains(search) ||
                                     t.Email.Contains(search) ||
                                     t.Expertise.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(t => t.Status == status);
        }

        int totalRecords = await query.CountAsync();

        List<MentorResponse> pagedData = await query
            .Skip((mentorSearchParameters.PageNumber - 1) * mentorSearchParameters.PageSize)
            .Take(mentorSearchParameters.PageSize)
            .Select(t => new MentorResponse(t))
            .ToListAsync();

        return new PagedResponse<MentorResponse>(pagedData, totalRecords, mentorSearchParameters.PageNumber, mentorSearchParameters.PageSize);
    }

    public async Task<MentorResponse> GetById(int Id)
    {
        Mentor? mentor = await _context.Mentors.FindAsync(Id);
        if (mentor == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", Id);
            throw new NotFoundException($"Mentor not found with Id: {Id}");
        }
        return new MentorResponse(mentor);
    }

    public async Task<MentorResponse> Create(CreateMentorRequest createMentorRequest)
    {
        Mentor mentor = new Mentor(createMentorRequest);
        await _context.Mentors.AddAsync(mentor);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Mentor event: {ActionEvent} occurred for MentorId: {MentorId}, Email: {Email}", "Created", mentor.Id, mentor.Email);
        return new MentorResponse(mentor);
    }

    public async Task<MentorResponse> Update(int Id, UpdateMentorRequest updateMentorRequest)
    {
        Mentor? mentor = await _context.Mentors.FindAsync(Id);
        if (mentor == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", Id);
            throw new NotFoundException($"Mentor not found with Id: {Id}");
        }

        mentor.FirstName = updateMentorRequest.FirstName;
        mentor.LastName = updateMentorRequest.LastName;
        mentor.Email = updateMentorRequest.Email;
        mentor.Expertise = updateMentorRequest.Expertise;
        mentor.Status = updateMentorRequest.Status;
        mentor.UpdatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
        
        await _context.SaveChangesAsync();
        _logger.LogInformation("Mentor event: {ActionEvent} occurred for MentorId: {MentorId}", "Updated", mentor.Id);
        return new MentorResponse(mentor);
    }

    public async Task<bool> Delete(int Id)
    {
        Mentor? mentor = await _context.Mentors.FindAsync(Id);
        if (mentor == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", Id);
            return false;
        }
        _context.Mentors.Remove(mentor);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Mentor event: {ActionEvent} occurred for MentorId: {MentorId}", "Deleted", Id);
        return true;
    }
}
