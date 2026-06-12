using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Services;
using TraineeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace TraineeManagement.Controllers;

[Authorize]
[ApiController]
[Route("/api/learning-tasks")]

public class LearningTaskController : ControllerBase
{
    private readonly ILearningTaskService _learningTaskService;

    public LearningTaskController(ILearningTaskService learningTaskService)
    {
        _learningTaskService = learningTaskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]LearningTaskSearchParameters learningTaskSearchParameters)
    {
        return Ok(await _learningTaskService.GetAll(learningTaskSearchParameters));
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetById(int Id)
    {
        LearningTaskResponse? learningTaskResponse = await _learningTaskService.GetById(Id);
        return learningTaskResponse == null ? NotFound() : Ok(learningTaskResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLearningTaskRequest createLearningTaskRequest)
    {
        LearningTaskResponse learningTaskResponse = await _learningTaskService.Create(createLearningTaskRequest);
        return Created("api/learning-tasks", learningTaskResponse);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult?> Update(int Id, UpdateLearningTask updateLearningTask)
    {
        LearningTaskResponse? learningTaskResponse = await _learningTaskService.Update(Id, updateLearningTask);
        return learningTaskResponse == null ? NotFound() : Ok(learningTaskResponse);
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> Delete(int Id)
    {
        bool deletedSuccessfully = await _learningTaskService.Delete(Id);
        return deletedSuccessfully ? NoContent() : NotFound();
    }
}