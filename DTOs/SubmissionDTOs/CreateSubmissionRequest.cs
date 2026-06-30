using
System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;

namespace TraineeManagement.DTOs;


public class CreateSubmissionRequest
{
    [Required]
    public int TaskAssignmentId {get; set;}

    [Required]
    [MaxLength(200)]
    public string SubmissionUrl {get; set;} = null!;

    [Required]
    [MaxLength(400)]
    public string Notes {get; set;} = null!;

    [Required]
    public DateTime SubmittedDate {get; set;}

     [Required]
     [AllowedValues([StringConstants.STATUS_SUBMITTED, StringConstants.STATUS_RESUBMITTED], ErrorMessage = "Invalid Status Value")]
     public string Status {get; set;} = null!;
}