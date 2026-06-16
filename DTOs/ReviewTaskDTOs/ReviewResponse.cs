using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class ReviewResponse
{

    public int SubmissionId {get; set;}
    
    public int MentorId {get; set;}
    
    
    public string Feedback {get; set;}
    
    
    public int Score {get; set;}

    
    public string ReviewStatus {get; set;}

    public DateTime ReviewedDate {get; set;}
    public ReviewResponse(Review s)
    {
        SubmissionId = s.SubmissionId;
        MentorId = s.MentorId;
        Feedback = s.Feedback;
        Score = s.Score;
        ReviewStatus = s.ReviewStatus;
        ReviewedDate = s.ReviewedDate;
    }
}