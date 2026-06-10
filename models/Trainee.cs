using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;

namespace TraineeManagement.Models;

public class Trainee
{
    [Key]
    public int Id { get; set; }

    public int UserId {get; set;}
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string TechStack { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Trainee(CreateTraineeRequest createTraineeRequest)
    {
        FirstName=createTraineeRequest.FirstName;
        LastName=createTraineeRequest.LastName;
        Email=createTraineeRequest.Email;
        TechStack=createTraineeRequest.TechStack;
        Status=createTraineeRequest.Status;
        DateTime dt = DateTime.Now;
        DateTime date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        CreatedDate = date;
        UpdatedDate = date;
    }

    Trainee(){ }
}