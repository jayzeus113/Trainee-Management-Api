using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class ReviewResponse
{
    public int Id {get; set;}
    public int SubmissionId {get; set;}
    
    public int MentorId {get; set;}
    
    public string Feedback {get; set;}
    
    public int Score {get; set;}

    
    public string ReviewStatus {get; set;}

    public DateTime ReviewedDate {get; set;}
    public ReviewResponse(Review r)
    {
        Id = r.Id;
        SubmissionId = r.SubmissionId;
        MentorId = r.MentorId;
        Feedback = r.Feedback;
        Score = r.Score;
        ReviewStatus = r.ReviewStatus;
        ReviewedDate = r.ReviewedDate;
    }
}