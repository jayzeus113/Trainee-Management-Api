using
System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class UserResponse
{
    public int Id {get; set;}
    
    public string Username {get; set;} = null!;

    public string Role {get; set;} = null!;
}