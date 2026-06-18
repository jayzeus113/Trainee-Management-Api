using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class TraineeResponse
{
    public long Id {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string TechStack { get; set; }
    public string Status { get; set; }

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
}