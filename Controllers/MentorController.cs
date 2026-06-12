using Microsoft.AspNetCore.Mvc;
using TraineeManagement.DTOs;
using TraineeManagement.Services;
using Microsoft.AspNetCore.Authorization;

namespace
TraineeManagement.Controllers;

[Authorize]
[ApiController]
[Route("api/mentors")]
public class MentorController : ControllerBase
{

    private readonly IMentorService _mentorService;

    public MentorController(IMentorService mentorService)
    {
        _mentorService = mentorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MentorSearchParameters mentorSearchParameters)
    {
        PagedResponse<MentorResponse> response = await  _mentorService.GetAll(mentorSearchParameters);
        return Ok(response);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetById(int Id)
    {
        MentorResponse? response = await _mentorService.GetById(Id);
        if(response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMentorRequest createMentorRequest)
    {
        MentorResponse response = await _mentorService.Create(createMentorRequest);
        return Created("api/Mentor", response);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update(int Id, UpdateMentorRequest updateMentorRequest)
    {
        MentorResponse? mentorResponse = await _mentorService.Update(Id, updateMentorRequest);
        return mentorResponse == null ? NotFound() : Ok(mentorResponse);
    }

    [HttpDelete("{Id:int}")]

    public async Task<IActionResult> Delete(int Id)
    {
        bool res = await _mentorService.Delete(Id);
        return res ? NoContent() : NotFound();
    }
}