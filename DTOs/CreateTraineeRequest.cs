using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class CreateTraineeRequest
{
    [Required]
    [MaxLength(50)]
    public string FirstName {get; set;}

    [Required]
    [MaxLength(50)]
    public string LastName {get; set;}

    [Required]
    [EmailAddress]
    public string Email {get; set;}

    [Required]
    public string TechStack {get; set;}
    
    [Required]
    public string Status {get; set;}
}