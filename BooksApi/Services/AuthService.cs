using BooksApi.DTOs.Auth;
using BooksApi.Models;
using BooksApi.Repositories.Interfaces;
using BooksApi.Services.Interfaces;

namespace BooksApi.Services
{
  public class AuthService : IAuthService
  {
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _tokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenService tokenService)
    {
      _userRepository = userRepository;
      _passwordHasher = passwordHasher;
      _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
      var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username);
      if (existingUser != null)
      {
        throw new InvalidOperationException("Username already exists");
      }

      var user = new User
      {
        Username = request.Username,
        Email = request.Email,
        PasswordHash = _passwordHasher.HashPassword(request.Password),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };

      var createdUser = await _userRepository.CreateUserAsync(user);

      var token = _tokenService.GenerateToken(createdUser.Id, createdUser.Username);

      return new AuthResponse
      {
        UserId = createdUser.Id,
        Username = createdUser.Username,
        Email = createdUser.Email,
        Token = token
      };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
      var user = await _userRepository.GetUserByUsernameAsync(request.Username);
      if (user == null)
      {
        throw new InvalidOperationException("Invalid username or password");
      }

      if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
      {
        throw new InvalidOperationException("Invalid username or password");
      }

      var token = _tokenService.GenerateToken(user.Id, user.Username);

      return new AuthResponse
      {
        UserId = user.Id,
        Username = user.Username,
        Email = user.Email,
        Token = token
      };
    }
  }
}
