using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Models;
using TraineeManagement.DTOs;

namespace
TraineeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TraineeController : ControllerBase
{
    private static int UID = 0;
    private static List<Trainee> trainees = new List<Trainee>
    {
        new Trainee{Id=0, FirstName="Jayprakash", LastName="Yadav", Email="abc@gmail.com", TechStack=".NET", Status="Active", CreatedDate=DateTime.Now, UpdatedDate = DateTime.Now}
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = trainees.Select(t => new TraineeResponse{
            FirstName= t.FirstName,
            LastName= t.LastName,
            Email = t.Email,
            TechStack = t.TechStack,
            Status = t.Status
        });
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var trainee = trainees.FirstOrDefault(x => x.Id == id);
        if(trainee == null) return NotFound();
        var response = new TraineeResponse{
            FirstName= trainee.FirstName,
            LastName= trainee.LastName,
            Email = trainee.Email,
            TechStack = trainee.TechStack,
            Status = trainee.Status
        };
        return Ok(response);
    }

    [HttpPost]
    public Trainee Create(CreateTraineeRequest createTraineeRequest)
    {
        Trainee trainee = new Trainee
        {
            Id= UID,
            FirstName= createTraineeRequest.FirstName,
            LastName= createTraineeRequest.LastName,
            Email = createTraineeRequest.Email,
            TechStack = createTraineeRequest.TechStack,
            Status = createTraineeRequest.Status,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.UtcNow
        };
        UID++;
        return trainee;
    }
}