using BooksApi.Models;

namespace BooksApi.Services.Interfaces
{
  public interface IJwtTokenService
  {
    string GenerateToken(User user);
  }
}
