using TraineeManagement.DTOs;
using TraineeManagement.Extensions;

namespace TraineeManagement.Models;

public class LearningTask
{
    public int Id {get; set;}

    public string Title {get; set;} = "";

    public string Description {get; set;} = "";

    public string ExpectedTechStack {get; set;} = "";

    public DateTime DueDate {get; set;}

    public string Status {get; set;} = string.Empty;

    public DateTime CreatedDate {get; set;}
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