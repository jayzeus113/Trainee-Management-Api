using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Exceptions;

namespace TraineeManagement.Services;
public class ReviewService : IReviewService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(AppDbContext context, ILogger<ReviewService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ReviewResponse>> GetAll()
    {
        return await _context.Reviews.Select(r => new ReviewResponse(r)).ToListAsync();
    }
    public async Task<ReviewResponse> GetById(int Id)
    {
        Review? review = await _context.Reviews.FindAsync(Id);
        if(review == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Review", Id);
            throw new NotFoundException($"Review not found with Id: {Id}");
        }
        return new ReviewResponse(review);
    }

    public async Task<ReviewResponse> Create(CreateReviewRequest createReviewRequest)
    {
        bool submissionExists = await _context.Submissions.AnyAsync(s => s.Id == createReviewRequest.SubmissionId);

        if(!submissionExists)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", createReviewRequest.SubmissionId);
            throw new NotFoundException($"Submission not found with Id: {createReviewRequest.SubmissionId}");
        }

        bool mentorExists = await _context.Mentors.AnyAsync(m => m.Id == createReviewRequest.MentorId);

        if(!mentorExists)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Identifier: {Identifier}", "Mentor", createReviewRequest.MentorId);
            throw new NotFoundException($"Mentor not found with Id: {createReviewRequest.MentorId}");
        }

        Review review = new Review(createReviewRequest);
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Review event: {ActionEvent} occured for {ReviewId}", "Created", review.Id);
        return new ReviewResponse(review);
    }
}