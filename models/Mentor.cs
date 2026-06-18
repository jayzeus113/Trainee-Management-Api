using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;
using TraineeManagement.Extensions;

namespace TraineeManagement.Models;

public class Mentor
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Expertise { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Mentor(CreateMentorRequest createMentorRequest)
    {
        FirstName=createMentorRequest.FirstName;
        LastName=createMentorRequest.LastName;
        Email=createMentorRequest.Email;
        Expertise=createMentorRequest.Expertise;
        Status=createMentorRequest.Status;
        CreatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
        UpdatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
    }

    Mentor(){ }
}