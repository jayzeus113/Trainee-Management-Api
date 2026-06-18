using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class MentorResponse
{
    public long Id {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Expertise { get; set; }
    public string Status { get; set; }

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
}