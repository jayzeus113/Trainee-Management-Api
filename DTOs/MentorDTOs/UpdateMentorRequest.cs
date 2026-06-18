using System.ComponentModel.DataAnnotations;


namespace TraineeManagement.DTOs;
public class UpdateMentorRequest
{
    [Required]
    [MaxLength(50)]
    public string FirstName {get; set;} = "";

    [Required]
    [MaxLength(50)]
    public string LastName {get; set;} = "";

    [Required]
    [EmailAddress]
    public string Email {get; set;} = "";

    [Required]
    public string Expertise {get; set;} = "";
    
    [Required]
    [AllowedValues(["Active", "Inactive"], ErrorMessage="Invalid status value")]
    public string Status {get; set;} = "";
}