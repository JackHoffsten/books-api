using BooksApi.Models;

namespace BooksApi.Repositories.Interfaces
{
  public interface IUserRepository
  {
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task SaveChangesAsync();
  }
}
