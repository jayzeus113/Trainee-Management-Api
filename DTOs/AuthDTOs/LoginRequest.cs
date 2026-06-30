using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class LoginRequest
{
    [Required]
    public string UserName {get; set;} = null!;

    [Required]
    public string Password {get; set;} = null!;
}