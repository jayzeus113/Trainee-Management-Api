using Microsoft.AspNetCore.Mvc;
using TraineeManagement.DTOs;
using TraineeManagement.Services;
using Microsoft.AspNetCore.Authorization;

namespace
TraineeManagement.Controllers;

// [Authorize]
[ApiController]
[Route("api/trainees")]
public class TraineeController : ControllerBase
{

    private readonly ITraineeService _traineeService;

    public TraineeController(ITraineeService traineeService)
    {
        _traineeService = traineeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TraineeSearchParameters traineeSearchParameters)
    {
        PagedResponse<TraineeResponse> response = await  _traineeService.GetAll(traineeSearchParameters);
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