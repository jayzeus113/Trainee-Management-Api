using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class CreateTaskAssignmentRequest
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
     [AllowedValues("Assigned", "InProgress", "Submitted", "Reviewed", "Completed", ErrorMessage = "Invalid Status Value")]
     public string Status {get; set;} = "";

     [Required]
     [MaxLength(100)]
     public string Remarks {get; set;} = "";

}