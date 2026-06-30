using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;
using TraineeManagement.Extensions;
namespace TraineeManagement.Models;
public class Review
{
    [Key]
    public int Id {get; set;}
    
    [Required]
    public int SubmissionId {get; set;}

    [Required]
    public int MentorId {get; set;}

    [Required]
    public string Feedback {get; set;} = null!;

    [Required]
    public int Score {get; set;}

    [Required]
    public string ReviewStatus {get; set;} = null!;

    [Required]
    public DateTime ReviewedDate {get; set;}

    public Review(CreateReviewRequest createReview)
    {
        SubmissionId = createReview.SubmissionId;
        MentorId = createReview.MentorId;
        Feedback = createReview.Feedback;
        Score = createReview.Score;
        ReviewStatus = createReview.ReviewStatus;
        ReviewedDate = createReview.ReviewedDate.ToUtcSecondPrecision();
    }
    Review() {}
}