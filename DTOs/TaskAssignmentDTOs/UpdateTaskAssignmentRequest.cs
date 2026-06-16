using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class UpdateTaskAssignmentRequest
{
     [Required]
     [AllowedValues("Assigned", "InProgress", "Submitted", "Reviewed", "Completed", ErrorMessage = "Invalid Status Value")]
     public string Status {get; set;} = "";
}