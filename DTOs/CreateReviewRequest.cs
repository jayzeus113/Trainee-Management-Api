using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class CreateReviewRequest
{
    [Required]
    public int SubmissionId {get; set;}
    
    [Required]
    public int MentorId {get; set;}
    
    [Required]
    [MaxLength(100)]
    public string Feedback {get; set;} = "";
    
    [Required]
    public int Score {get; set;}

    [Required]
    [AllowedValues("Rejected", "Accepted", "ChangesRequired", ErrorMessage = "Invalid ReviewStatus Value")]
    public string ReviewStatus {get; set;} = "";

    public DateTime ReviewedDate {get; set;}
}