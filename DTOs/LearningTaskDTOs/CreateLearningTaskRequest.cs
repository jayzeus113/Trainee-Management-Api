using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class CreateLearningTaskRequest {

    [Required]
    [MaxLength(50)]
    public string Title {get; set;} = "";

    [Required]
    [MaxLength(250)]
    public string Description {get; set;} = "";

    [Required]
    [MaxLength(50)]
    public string ExpectedTechStack {get; set;} = "";

    [Required]
    public DateTime DueDate {get; set;}

    [Required]
    [AllowedValues("Draft", "Published", "Closed", ErrorMessage = "Invalid status value")]
    public string Status {get; set;} = "";
}