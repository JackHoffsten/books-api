using Microsoft.EntityFrameworkCore;
using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Repositories.Interfaces;

namespace BooksApi.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly BooksDbContext _context;

    public UserRepository(BooksDbContext context)
    {
      _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<User> CreateOrUpdateUserAsync(User user)
    {
      var existingUser = await GetUserByIdAsync(user.Id);

      if (existingUser == null)
      {
        _context.Users.Add(user);
      }
      else
      {
        _context.Users.Update(user);
      }
      await _context.SaveChangesAsync();

      return user;
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
