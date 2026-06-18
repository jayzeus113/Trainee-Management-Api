using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class UpdateSubmissionRequest
{
    [Required]
    public int TaskAssignmentId {get; set;}

    [Required]
    [MaxLength(100)]
    public string SubmissionUrl {get; set;} = "";

    [Required]
    [MaxLength(100)]
    public string Notes {get; set;} = "";

    [Required]
    public DateTime SubmittedDate {get; set;}

    [Required]
    [AllowedValues("Submitted", "Resubmitted", ErrorMessage = "Invalid Status Value")]
    public string Status {get; set;} = "";
}