using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class CreateLearningTaskRequest {
    [Required]
    public string Title {get; set;} = "";

    [Required]
    public string Description {get; set;} = "";

    [Required]
    public string ExpectedTechStack {get; set;} = "";

    [Required]
    public DateTime DueDate {get; set;}

    [Required]
    [AllowedValues("Draft", "Published", "Closed", ErrorMessage = "Invalid Status Value")]
    public string Status {get; set;} = "";
}