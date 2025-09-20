using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBPortal.Api.Services;
using WBPortal.Api.DTOs;
using WBPortal.Api.Models;
using AutoMapper;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;
    private readonly IMapper _mapper;

    public FeedbackController(IFeedbackService feedbackService, IMapper mapper)
    {
        _feedbackService = feedbackService;
        _mapper = mapper;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpGet("project/{projectId}")]
    public IActionResult GetFeedbacksByProject(int projectId)
    {
        var feedbacks = _feedbackService.GetFeedbacksByProject(projectId);
        var dto = _mapper.Map<List<FeedbackResponseDTO>>(feedbacks);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult CreateFeedback([FromBody] FeedbackCreateDTO dto)
    {
        var feedback = _mapper.Map<Feedback>(dto);
        feedback.UserId = GetUserId();
        feedback.Timestamp = DateTime.UtcNow;

        _feedbackService.CreateFeedback(feedback);
        return Ok("Feedback added successfully");
    }
}
