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
      try
      {
        var response = await _authService.RegisterAsync(request);
        return Ok(response);
      }
      catch (ApiException ex)
      {
        return BadRequest(CreateErrorResponse(ex));
      }
      catch (Exception ex)
      {
        return StatusCode(500, CreateErrorResponse(ex));
      }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
      try
      {
        var response = await _authService.LoginAsync(request);
        return Ok(response);
      }
      catch (ApiException ex)
      {
        return BadRequest(CreateErrorResponse(ex));
      }
      catch (Exception ex)
      {
        return StatusCode(500, CreateErrorResponse(ex));
      }
    }
  }
}
