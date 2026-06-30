using System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;

namespace TraineeManagement.DTOs;

public class UpdateReviewRequest
{ 
    [Required]
    public int SubmissionId {get; set;}
    
    
    [Required]
    public int MentorId {get; set;}
    
    [Required]
    [MaxLength(200)]
    public string Feedback {get; set;} = null!;
    
    public int Score {get; set;}

    [Required]
    [AllowedValues([StringConstants.STATUS_REJECTED, StringConstants.STATUS_ACCEPTED, StringConstants.STATUS_CHANGES_REQUIRED], ErrorMessage = "Invalid ReviewStatus Value")]
    public string ReviewStatus {get; set;} = null!;

    public DateTime ReviewedDate {get; set;}
}