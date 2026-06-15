using TraineeManagement.DTOs;
namespace TraineeManagement.Models;
public class Review
{
    public int Id {get; set;}

    public int SubmissionId {get; set;}
    public int MentorId {get; set;}
    public string Feedback {get; set;} = "";

    public int Score {get; set;}


    public string ReviewStatus {get; set;} = "";

    public DateTime ReviewedDate {get; set;}

    public Review(CreateReviewRequest createReview)
    {
        SubmissionId = createReview.SubmissionId;
        MentorId = createReview.MentorId;
        Feedback = createReview.Feedback;
        Score = createReview.Score;
        ReviewStatus = createReview.ReviewStatus;
        ReviewedDate = createReview.ReviewedDate;
    }
    Review() {}
}