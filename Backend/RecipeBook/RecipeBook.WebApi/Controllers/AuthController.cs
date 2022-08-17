using Microsoft.AspNetCore.Mvc;
using RecipeBook.BLL.Models.Auth;
using RecipeBook.BLL.Services.Interfaces;

namespace RecipeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest registerUser)
    {
        return Ok(await _authService.RegisterAsync(registerUser));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Register([FromBody] UserLoginRequest loginUser)
    {
        return Ok(await _authService.LoginAsync(loginUser));
    }
}