using Microsoft.AspNetCore.Mvc;
using BooksApi.DTOs.Auth;
using BooksApi.Services.Interfaces;
using BooksApi.Exceptions;

namespace BooksApi.Controllers
{
  [Route("api/auth")]
  public class AuthController : BaseController
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
      var response = await _authService.RegisterAsync(request);
      return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
      var response = await _authService.LoginAsync(request);
      return Ok(response);
    }
  }
}
