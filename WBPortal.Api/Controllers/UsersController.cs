using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBPortal.Api.Services;
using WBPortal.Api.DTOs;
using WBPortal.Api.Models;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        var dto = _mapper.Map<List<UserResponseDTO>>(users);
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null) return NotFound();

        var dto = _mapper.Map<UserResponseDTO>(user);
        dto.Projects = _mapper.Map<List<ProjectResponseDTO>>(user.Projects);
        dto.Feedbacks = _mapper.Map<List<FeedbackResponseDTO>>(user.Feedbacks);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateDTO dto)
    {
        var user = _mapper.Map<User>(dto);
        _userService.CreateUser(user);
        return Ok("User created successfully");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserUpdateDTO dto)
    {
        var user = _mapper.Map<User>(dto);
        user.Id = id;
        _userService.UpdateUser(user);
        return Ok("User updated successfully");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return Ok("User deleted successfully");
    }
}
