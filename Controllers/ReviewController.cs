using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Services;
using TraineeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace TraineeManagement.Controllers;

[Authorize]
[ApiController]
[Route("/api/review")]

public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ReviewResponse> reviewsResponses = await _reviewService.GetAll();
        return Ok(reviewsResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetById(int Id)
    {
        ReviewResponse reviewResponse = await _reviewService.GetById(Id);
        return Ok(reviewResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewRequest createReviewRequest)
    {
        ReviewResponse reviewResponse = await _reviewService.Create(createReviewRequest);
        return Created("api/reviews", reviewResponse);
    }
}