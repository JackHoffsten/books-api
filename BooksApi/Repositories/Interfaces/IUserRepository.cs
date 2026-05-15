using BooksApi.Models;

namespace BooksApi.Repositories.Interfaces
{
  public interface IUserRepository
  {
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    Task<User> CreateOrUpdateUserAsync(User user);
    Task SaveChangesAsync();
  }
}
