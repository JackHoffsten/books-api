namespace BooksApi.Exceptions.Auth
{
  public class InvalidRefreshTokenException : ApiException
  {
    public override string Title => "Authentication Error";
    public override ErrorCode Code => ErrorCode.INVALID_REFRESH_TOKEN;
    public override int StatusCode => 400;
    public InvalidRefreshTokenException() : base("Invalid refresh token.") { }
  }
}