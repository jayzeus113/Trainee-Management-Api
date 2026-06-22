using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class UpdateReviewRequest
{ 
    [Range(1, int.MaxValue, ErrorMessage = "Invalid SubmissionId")]
    public int SubmissionId {get; set;}
    
    
    [Range(1, int.MaxValue, ErrorMessage = "Invalid MentorId")]
    public int MentorId {get; set;}
    
    [Required]
    [MaxLength(100)]
    public string Feedback {get; set;} = "";
    
    public int Score {get; set;}

    [Required]
    [AllowedValues("Rejected", "Accepted", "ChangesRequired", ErrorMessage = "Invalid ReviewStatus Value")]
    public string ReviewStatus {get; set;} = "";

    public DateTime ReviewedDate {get; set;}
}