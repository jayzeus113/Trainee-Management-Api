using System.Security.Claims;
using System.Security.Cryptography;
using TraineeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TraineeManagement.Services;
using TraineeManagement.Data;
using System.Net.Mime;

namespace TraineeManagement.Controllers;

[Authorize]
[ApiController]
[Route("api/submission-files")]
public class SubmissionsController : ControllerBase
{
    private readonly ISubmissionService _submissionService;
    private readonly AppDbContext _context; 

    public SubmissionsController(ISubmissionService submissionService, AppDbContext context)
    {
        _submissionService = submissionService;
        _context = context;
    }

    [HttpGet("{SubmissionId:int}")]
    public async Task<IActionResult> DownloadFile(int SubmissionId)
    {
        FileStream fileStream = await _submissionService.DownloadFile(SubmissionId);
        string fileName = Path.GetFileName(fileStream.Name);
        string extension = Path.GetExtension(fileName);
        string contentType = $"application/{extension.Substring(1)}";
        return File(fileStream, contentType, fileName);
    }

    [HttpDelete("{SubmissionId:int}")]
    public async Task<IActionResult> DeleteFile(int SubmissionId)
    {
        bool isDeleted = await _submissionService.DeleteFile(SubmissionId);
        return NoContent();
    }
}
