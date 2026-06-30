using System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;

namespace TraineeManagement.DTOs;

public class CreateLearningTaskRequest {

    [Required]
    [MaxLength(100)]
    public string Title {get; set;} = null!;

    [Required]
    [MaxLength(400)]
    public string Description {get; set;} = null!;

    [Required]
    [MaxLength(300)]
    public string ExpectedTechStack {get; set;} = null!;

    [Required]
    public DateTime DueDate {get; set;}

    [Required]
    [AllowedValues([StringConstants.STATUS_DRAFT, StringConstants.STATUS_PUBLISHED, StringConstants.STATUS_CLOSED], ErrorMessage = "Invalid status value")]
    public string Status {get; set;} = null!;
}