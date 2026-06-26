using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Services;
using TraineeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace TraineeManagement.Controllers;

[Authorize]
[ApiController]
[Route("/api/processing-jobs")]

public class ProcessingJobController : ControllerBase
{
    private readonly IProcessingJobService _processingJobService;

    public ProcessingJobController(IProcessingJobService processingJobService)
    {
        _processingJobService = processingJobService;
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetById(int Id)
    {
        ProcessingJobResponse? processingJobResponse = await _processingJobService.GetById(Id);
        return processingJobResponse == null ? NotFound() : Ok(processingJobResponse);
    }
}