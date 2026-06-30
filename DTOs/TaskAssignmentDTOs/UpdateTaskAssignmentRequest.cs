using
System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;

namespace TraineeManagement.DTOs;

public class UpdateTaskAssignmentRequest
{
    [Required]
    public int TraineeId {get; set;}

    [Required]
    public int MentorId {get; set;}

    [Required]
    public int LearningTaskId {get; set;}

    [Required]
    public DateTime AssigenedDate {get; set;}

    [Required]
    public DateTime DueDate {get; set;}

    [Required]
    [AllowedValues([StringConstants.STATUS_ASSIGNED, StringConstants.STATUS_IN_PROGRESS, StringConstants.STATUS_SUBMITTED, StringConstants.STATUS_REVIEWED, StringConstants.STATUS_COMPLETED], ErrorMessage = "Invalid Status Value")]
    public string Status {get; set;} = null!;

    [Required]
    [MaxLength(300)]
    public string Remarks {get; set;} = null!;
}