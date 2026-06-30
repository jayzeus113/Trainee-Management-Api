using System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;


namespace TraineeManagement.DTOs;
public class UpdateTraineeRequest
{
    [Required]
    [MaxLength(50)]
    public string FirstName {get; set;} = null!;

    [Required]
    [MaxLength(50)]
    public string LastName {get; set;} = null!;

    [Required]
    [EmailAddress]
    public string Email {get; set;} = null!;

    [Required]
    [MaxLength(300)]
    public string TechStack {get; set;} = null!;
    
    [Required]
    [AllowedValues([StringConstants.STATUS_ACTIVE, StringConstants.STATUS_INACTIVE, StringConstants.STATUS_COMPLETED], ErrorMessage="Invalid status value")]
    public string Status {get; set;} = null!;
}