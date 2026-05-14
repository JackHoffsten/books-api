using BooksApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksApi.Controllers
{
  [ApiController]
  public class BaseController : ControllerBase
  {
    protected int GetUserId()
    {
      var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
      if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
      {
        throw new UnauthorizedAccessException("User ID not found in token");
      }
      return userId;
    }
    protected ProblemDetails CreateErrorResponse(Exception ex, string? code = null)
    {
      var apiException = ex as ApiException;
      var statusCode = apiException?.StatusCode ?? 500;
      var errorCode = code ?? apiException?.Code ?? "INTERNAL_ERROR";

      return new ProblemDetails
      {
        Title = "Error",
        Detail = ex.Message,
        Status = statusCode,
        Extensions =
        {
          ["code"] = errorCode,
          ["timestamp"] = DateTime.UtcNow
        }
      };
    }
  }
}