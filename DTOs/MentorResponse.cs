using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class MentorResponse
{
    public long Id {get; set;}
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Expertise { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public DateTime CreatedDate {get; set;}

    public DateTime UpdatedDate {get; set;}

    public MentorResponse(Mentor m)
    {
        Id = m.Id;
        FirstName = m.FirstName;
        LastName = m.LastName;
        Email = m.Email;
        Expertise = m.Expertise;
        Status = m.Status;
        CreatedDate = m.CreatedDate;
        UpdatedDate = m.UpdatedDate;
    }

    public MentorResponse(){}
}