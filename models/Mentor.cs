using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;
using TraineeManagement.Extensions;

namespace TraineeManagement.Models;

public class Mentor
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Expertise { get; set; } = null!;
    [Required]
    public string Status { get; set; } = null!;
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
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