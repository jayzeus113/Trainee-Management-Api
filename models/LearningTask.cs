using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;
using TraineeManagement.Extensions;

namespace TraineeManagement.Models;

public class LearningTask
{
    [Key]
    public int Id {get; set;}
    
    [Required]
    public string Title {get; set;} = null!;

    [Required]
    public string Description {get; set;} = null!;
    
    [Required]
    public string ExpectedTechStack {get; set;} = null!;

    [Required]
    public DateTime DueDate {get; set;}

    [Required]
    public string Status {get; set;} = null!;

    [Required]
    public DateTime CreatedDate {get; set;}

    [Required]
    public DateTime UpdatedDate {get; set;}

    public LearningTask(CreateLearningTaskRequest createLearningTaskRequest) {
        Title = createLearningTaskRequest.Title;
        Description = createLearningTaskRequest.Description;
        ExpectedTechStack = createLearningTaskRequest.ExpectedTechStack;
        DueDate = createLearningTaskRequest.DueDate.ToUtcSecondPrecision();
        Status = createLearningTaskRequest.Status;
        CreatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
        UpdatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
    }
    LearningTask() {}
}