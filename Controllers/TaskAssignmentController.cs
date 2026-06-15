using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Services;
using TraineeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace TraineeManagement.Controllers;

[Authorize]
[ApiController]
[Route("/api/task-assignments")]

public class TaskAssignmentController : ControllerBase
{
    private readonly ITaskAssignmentService _taskAssignmentService;

    public TaskAssignmentController(ITaskAssignmentService taskAssignmentService)
    {
        _taskAssignmentService = taskAssignmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<TaskAssignmentResponse> taskAssignmentResponses = await _taskAssignmentService.GetAll();
        return Ok(taskAssignmentResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetById(int Id)
    {
        TaskAssignmentResponse taskAssignmentResponse = await _taskAssignmentService.GetById(Id);
        return Ok(taskAssignmentResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskAssignmentRequest createTaskAssignmentRequest)
    {
        TaskAssignmentResponse taskAssignmentResponse = await _taskAssignmentService.Create(createTaskAssignmentRequest);
        return Created("api/task-assignments", taskAssignmentResponse);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update(int Id, UpdateTaskAssignmentRequest updateTaskAssignmentRequest)
    {
        TaskAssignmentResponse taskAssignmentResponse = await _taskAssignmentService.Update(Id, updateTaskAssignmentRequest);
        return Ok(taskAssignmentResponse);
    }
}