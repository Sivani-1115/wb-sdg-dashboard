using Microsoft.AspNetCore.Mvc;
using WBPortal.Api.Services;
using WBPortal.Api.DTOs;
using WBPortal.Api.Models;
using WBPortal.Api.Data;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };
        _authService.Register(user);
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = _authService.Login(request.Username, request.Password);
                Console.WriteLine(token);
            return Ok(new AuthResponse { Token = token });
        }
        catch
        {
            return Unauthorized("Invalid credentials");
        }
    }
}
