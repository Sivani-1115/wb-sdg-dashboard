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
        try
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || 
                string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Role))
            {
                return BadRequest("All fields are required");
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };
            _authService.Register(user);
            Console.WriteLine($"User registered successfully: {request.Username}");
            return Ok("User registered successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Registration error: {ex.Message}");
            return BadRequest($"Registration failed: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required");
            }

            var result = _authService.Login(request.Username, request.Password);
            Console.WriteLine($"Login successful for user: {request.Username}");
            return Ok(new AuthResponse { Token = result.Token, Role = result.Role });
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Login failed - {ex.Message}");
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            return Unauthorized("Invalid credentials");
        }
    }
}
