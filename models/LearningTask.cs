using TraineeManagement.DTOs;
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
        DueDate = createLearningTaskRequest.DueDate;
        Status = createLearningTaskRequest.Status;
        DateTime dt = new DateTime();
        dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        CreatedDate = dt;
        UpdatedDate = dt;
    }
    LearningTask() {}
}