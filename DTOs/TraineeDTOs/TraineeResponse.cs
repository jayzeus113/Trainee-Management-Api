using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class TraineeResponse
{
    public long Id {get; set;}
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string TechStack { get; set; } = null!;
    public string Status { get; set; } = null!;

    public DateTime CreatedDate {get; set;}

    public DateTime UpdatedDate {get; set;}

    public TraineeResponse() {}
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
}