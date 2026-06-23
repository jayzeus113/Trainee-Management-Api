using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Services;
using TraineeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace TraineeManagement.Controllers;

[Authorize]
[ApiController]
[Route("/api/submission")]

public class SubmissionController : ControllerBase
{
    private readonly ISubmissionService _submissionService;

    public SubmissionController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<SubmissionResponse> submissionsResponses = await _submissionService.GetAll();
        return Ok(submissionsResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetById(int Id)
    {
        SubmissionResponse submissionResponse = await _submissionService.GetById(Id);
        return Ok(submissionResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubmissionRequest createSubmissionRequest)
    {
        SubmissionResponse submissionResponse = await _submissionService.Create(createSubmissionRequest);
        return Created("api/submissions", submissionResponse);
    }

     [HttpPost("{SubmissionId:int}/files")]
    public async Task<IActionResult> UploadFile([FromRoute] int SubmissionId, [FromForm] CreateSubmissionFileRequest createSubmissionFileRequest )
    {
        // int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        SubmissionFileResponse submissionFileResponse = await _submissionService.UploadFile(1, SubmissionId, createSubmissionFileRequest);
        return Accepted($"/api/submissions/{SubmissionId}/files", submissionFileResponse);
    }
}