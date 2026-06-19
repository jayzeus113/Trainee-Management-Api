using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class CreateMentorRequest
{
    [Required]
    [MaxLength(50)]
    public string FirstName {get; set;} = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName {get; set;} = string.Empty;

    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Expertise {get; set;} = string.Empty;
    
    [Required]
    [AllowedValues(["Active", "Inactive"], ErrorMessage="Invalid status value")]
    public string Status {get; set;} = string.Empty;
}