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
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        IEnumerable<TraineeResponse> response = await  _traineeService.GetAll(search);
        return Ok(response);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetById(int Id)
    {
        TraineeResponse? response = await _traineeService.GetById(Id);
        if(response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTraineeRequest createTraineeRequest)
    {
        TraineeResponse response = await _traineeService.Create(createTraineeRequest);
        return Created("api/Trainee", response);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        TraineeResponse? traineeResponse = await _traineeService.Update(Id, updateTraineeRequest);
        return traineeResponse == null ? NotFound() : Ok(traineeResponse);
    }

    [HttpDelete("{Id:int}")]

    public async Task<IActionResult> Delete(int Id)
    {
        bool res = await _traineeService.Delete(Id);
        return res ? NoContent() : NotFound();
    }
}