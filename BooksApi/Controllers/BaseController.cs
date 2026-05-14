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
  }
}