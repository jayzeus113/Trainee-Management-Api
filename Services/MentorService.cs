using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;

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
        IQueryable<Mentor> query = _context.Mentors.AsQueryable();
        string search = mentorSearchParameters.Search!;
        string status = mentorSearchParameters.Status!;

        if(!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.FirstName.ToLower().Contains(search) ||
            t.LastName.ToLower().Contains(search) ||
            t.Email.ToLower().Contains(search) ||
            t.Expertise.ToLower().Contains(search)
            );
        }

        if(!string.IsNullOrWhiteSpace(status))
            query = query.Where(t => t.Status == status);

        int totalRecords = await query.CountAsync();

        List<MentorResponse> pagedData = await query.Skip((mentorSearchParameters.PageNumber - 1) * mentorSearchParameters.PageSize)
        .Take(mentorSearchParameters.PageSize)
        .Select(t => new MentorResponse(t))
        .ToListAsync();
        

        return new PagedResponse<MentorResponse>(pagedData, totalRecords, mentorSearchParameters.PageNumber, mentorSearchParameters.PageSize);
    }
    public async Task<MentorResponse?> GetById(int Id)
    {
        Mentor? Mentor = await _context.Mentors.FindAsync(Id);
        if(Mentor == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", Id);
            return null;
        }
        return new MentorResponse(Mentor);
    }

    public async Task<MentorResponse> Create(CreateMentorRequest createMentorRequest)
    {
        Mentor Mentor = new Mentor(createMentorRequest);
        await _context.Mentors.AddAsync(Mentor);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Mentor event: {ActionEvent} occured for {MentorId}", "Created", Mentor.Id);
        return new MentorResponse(Mentor);
    }

    public async Task<MentorResponse?> Update(int Id, UpdateMentorRequest updateMentorRequest)
    {
        Mentor? Mentor = await _context.Mentors.FindAsync(Id);
        if(Mentor == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", Id);
            return null;
        }
        Mentor.FirstName = updateMentorRequest.FirstName;
        Mentor.LastName = updateMentorRequest.LastName;
        Mentor.Email = updateMentorRequest.Email;
        Mentor.Expertise = updateMentorRequest.Expertise;
        Mentor.Status = updateMentorRequest.Status;
        DateTime dt = DateTime.Now;
        DateTime date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        Mentor.UpdatedDate = date;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Mentor event: {ActionEvent} occurred for MentorId: {MentorId}", "Updated", Mentor.Id);
        return new MentorResponse(Mentor);
    }

    public async Task<bool> Delete(int Id)
    {
        Mentor? Mentor = await _context.Mentors.FindAsync(Id);
        if(Mentor == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", Id);
            return false;
        }
        _context.Mentors.Remove(Mentor);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Mentor event: {ActionEvent} occurred for MentorId: {MentorId}", "Updated", Id);
        return true;
    }
}