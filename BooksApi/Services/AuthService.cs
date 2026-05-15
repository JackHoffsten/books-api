using BooksApi.DTOs.Auth;
using BooksApi.Exceptions.Auth;
using BooksApi.Models;
using BooksApi.Repositories.Interfaces;
using BooksApi.Services.Interfaces;

namespace BooksApi.Services
{
  public class AuthService : IAuthService
  {
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService)
    {
      _userRepository = userRepository;
      _passwordHasher = passwordHasher;
      _jwtTokenService = jwtTokenService;
      _refreshTokenService = refreshTokenService;
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

      var createdUser = await _userRepository.CreateOrUpdateUserAsync(user);

      return await GetAuthResponseAsync(createdUser);
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

      return await GetAuthResponseAsync(user);
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
      var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
      if (user == null)
      {
        throw new InvalidRefreshTokenException();
      }

      if (user.RefreshTokenExpiry < DateTime.UtcNow)
      {
        throw new ExpiredRefreshTokenException();
      }

      return await GetAuthResponseAsync(user);
    }

    private async Task<AuthResponse> GetAuthResponseAsync(User user)
    {
      var accessToken = _jwtTokenService.GenerateToken(user);
      var refreshToken = _refreshTokenService.GenerateToken();

      user.RefreshToken = _refreshTokenService.HashToken(refreshToken);
      user.RefreshTokenExpiry = _refreshTokenService.GetTokenExpiryDate();

      await _userRepository.CreateOrUpdateUserAsync(user);

      return new AuthResponse
      {
        UserId = user.Id,
        Username = user.Username,
        Email = user.Email,
        AccessToken = accessToken,
        RefreshToken = refreshToken
      };
    }
  }
}
