using System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;

namespace TraineeManagement.DTOs;

public class CreateMentorRequest
{
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z\s\-']+$", ErrorMessage = "Last name can only contain letters, spaces, hyphens, and apostrophes.")]
    public string FirstName {get; set;} = null!;

    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z\s\-']+$", ErrorMessage = "Last name can only contain letters, spaces, hyphens, and apostrophes.")]
    public string LastName {get; set;} = null!;

    [Required]
    [EmailAddress]
    [MaxLength(500)]
    public string Email {get; set;} = null!;

    [Required]
    [MaxLength(300)]
    public string Expertise {get; set;} = null!;
    
    [Required]
    [AllowedValues([StringConstants.STATUS_ACTIVE, StringConstants.STATUS_INACTIVE], ErrorMessage="Invalid status value")]
    public string Status {get; set;} = null!;
}