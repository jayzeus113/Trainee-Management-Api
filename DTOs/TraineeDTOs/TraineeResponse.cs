using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class TraineeResponse
{
    public long Id {get; set;}
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TechStack { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public DateTime CreatedDate {get; set;}

    public DateTime UpdatedDate {get; set;}

    public TraineeResponse(Trainee t)
    {
        Id = t.Id;
        FirstName = t.FirstName;
        LastName = t.LastName;
        Email = t.Email;
        TechStack = t.TechStack;
        Status = t.Status;
        CreatedDate = t.CreatedDate;
        UpdatedDate = t.UpdatedDate;
    }

    public TraineeResponse(){}
}