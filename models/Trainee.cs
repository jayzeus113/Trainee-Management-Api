using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;
using TraineeManagement.Extensions;

namespace TraineeManagement.Models;

public class Trainee
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
    [MaxLength]
    public string TechStack { get; set; } = null!;
    
    [Required]
    public string Status { get; set; } = null!;
    
    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    public DateTime UpdatedDate { get; set; }

    public Trainee(CreateTraineeRequest createTraineeRequest)
    {
        FirstName=createTraineeRequest.FirstName;
        LastName=createTraineeRequest.LastName;
        Email=createTraineeRequest.Email;
        TechStack=createTraineeRequest.TechStack;
        Status=createTraineeRequest.Status;
        CreatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
        UpdatedDate = DateTime.UtcNow.ToUtcSecondPrecision();
    }

    Trainee(){ }
}