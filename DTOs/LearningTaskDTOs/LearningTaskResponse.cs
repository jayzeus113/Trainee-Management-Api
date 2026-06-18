using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class LearningTaskResponse
{
    public int Id {get; set;}

    public string Title {get; set;}

    public string Description {get; set;}

    public string ExpectedTechStack {get; set;}

    public DateTime DueDate {get; set;}

    public string Status {get; set;}

    public DateTime CreatedDate {get; set;}

    public DateTime UpdatedDate {get; set;}

    public LearningTaskResponse(LearningTask lt)
    {
        Id = lt.Id;
        Title = lt.Title;
        Description = lt.Description;
        ExpectedTechStack = lt.ExpectedTechStack;
        DueDate = lt.DueDate;
        Status = lt.Status;
        CreatedDate = lt.CreatedDate;
        UpdatedDate = lt.UpdatedDate;
    }
}