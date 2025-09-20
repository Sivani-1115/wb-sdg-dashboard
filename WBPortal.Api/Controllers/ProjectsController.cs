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
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    private string GetUserRole() => User.FindFirstValue(ClaimTypes.Role);

    [HttpGet]
    public IActionResult GetProjects()
    {
        var role = GetUserRole();
        var userId = GetUserId();
        IEnumerable<Project> projects = role switch
        {
            "Admin" => _projectService.GetAllProjects(),
            "Manager" => _projectService.GetProjectsByManager(userId),
            _ => _projectService.GetAllProjects()
        };

        var dto = _mapper.Map<List<ProjectResponseDTO>>(projects);
        foreach (var projectDto in dto)
        {
            var project = projects.First(p => p.Id == projectDto.Id);
            projectDto.Feedbacks = _mapper.Map<List<FeedbackResponseDTO>>(project.Feedbacks);
        }

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public IActionResult GetProjectById(int id)
    {
        var project = _projectService.GetProjectById(id);
        if (project == null) return NotFound();

        var dto = _mapper.Map<ProjectResponseDTO>(project);
        dto.Feedbacks = _mapper.Map<List<FeedbackResponseDTO>>(project.Feedbacks);
        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public IActionResult CreateProject([FromBody] ProjectCreateDTO dto)
    {
        var project = _mapper.Map<Project>(dto);
        _projectService.CreateProject(project);
        return Ok("Project created successfully");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public IActionResult UpdateProject(int id, [FromBody] ProjectUpdateDTO dto)
    {
        var project = _mapper.Map<Project>(dto);
        project.Id = id;
        _projectService.UpdateProject(project);
        return Ok("Project updated successfully");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteProject(int id)
    {
        _projectService.DeleteProject(id);
        return Ok("Project deleted successfully");
    }
}
