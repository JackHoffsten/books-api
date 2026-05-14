using BooksApi.DTOs.Auth;
using BooksApi.Exceptions;
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
      request.Username = request.Username.Trim();
      request.Email = request.Email.Trim().ToLower();

      if (request.Username.Length == 0)
      {
        throw new UsernameEmptyException();
      }

      if (request.Email.Length == 0)
      {
        throw new EmailEmptyException();
      }

      if (request.Password.Length < AuthConstants.MinPasswordLength) 
      {
        throw new PasswordTooShortException();
      }

      var existingUserByEmail = await _userRepository.GetUserByEmailAsync(request.Email);
      if (existingUserByEmail != null)
      {
        throw new EmailTakenException();
      }

      var existingUserByUsername = await _userRepository.GetUserByUsernameAsync(request.Username);
      if (existingUserByUsername != null)
      {
        throw new UsernameTakenException();
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
      request.Username = request.Username.Trim();

      var user = await _userRepository.GetUserByUsernameAsync(request.Username);
      if (user == null)
      {
        throw new InvalidCredentialsException();
      }

      if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
      {
        throw new InvalidCredentialsException();
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
