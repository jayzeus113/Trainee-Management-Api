using TraineeManagement.DTOs;

namespace TraineeManagement.Models;

public class Trainee
{
    private static int UID = 0;
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string TechStack { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Trainee(CreateTraineeRequest createTraineeRequest)
    {
        Id=UID;
        FirstName=createTraineeRequest.FirstName;
        LastName=createTraineeRequest.LastName;
        Email=createTraineeRequest.Email;
        TechStack=createTraineeRequest.TechStack;
        Status=createTraineeRequest.Status;
        CreatedDate=DateTime.UtcNow;
        UpdatedDate=DateTime.UtcNow;
        UID++;
    }
}