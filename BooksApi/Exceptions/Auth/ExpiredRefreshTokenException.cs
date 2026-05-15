namespace BooksApi.Exceptions.Auth
{
  public class ExpiredRefreshTokenException : ApiException
  {
    public override string Title => "Authentication Error";
    public override ErrorCode Code => ErrorCode.EXPIRED_REFRESH_TOKEN;
    public override int StatusCode => 400;
    public ExpiredRefreshTokenException() : base("Refresh token has expired.") { }
  }
}