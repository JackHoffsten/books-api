namespace BooksApi.Exceptions.Auth
{
  public class InvalidCredentialsException : ApiException
  {
    public override string Title => "Authentication Error";
    public override ErrorCode Code => ErrorCode.INVALID_CREDENTIALS;
    public override int StatusCode => 400;
    public InvalidCredentialsException() : base("Invalid username or password.") { }
  }
}