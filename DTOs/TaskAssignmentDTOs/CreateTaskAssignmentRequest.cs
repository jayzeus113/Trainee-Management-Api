using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class CreateTaskAssignmentRequest
{
    
    [Range(1, int.MaxValue, ErrorMessage = "Invalid TraineeId")]
    public int TraineeId {get; set;}

    
    [Range(1, int.MaxValue, ErrorMessage = "Invalid MentorId")]
    public int MentorId {get; set;}

    [Range(1, int.MaxValue, ErrorMessage = "Invalid LearningTaskId")]
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