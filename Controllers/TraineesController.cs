using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Services;

namespace
TraineeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TraineeController : ControllerBase
{

    private readonly ITraineeService _traineeService;

    public TraineeController(ITraineeService traineeService)
    {
        _traineeService = traineeService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _traineeService.GetAll();
        return Ok(response);
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetById(int Id)
    {
        var response = _traineeService.GetById(Id);
        if(response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateTraineeRequest createTraineeRequest)
    {
        TraineeResponse response = _traineeService.Create(createTraineeRequest);
        return Created("api/Trainee", response);
    }

    [HttpPut("{Id:int}")]
    public IActionResult Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        TraineeResponse ? traineeResponse = _traineeService.Update(Id, updateTraineeRequest);
        return traineeResponse == null ? NotFound() : Ok(traineeResponse);
    }

    [HttpDelete("{Id:int}")]

    public IActionResult Delete(int Id)
    {
        bool res = _traineeService.Delete(Id);
        return res ? NoContent() : NotFound();
    }


}