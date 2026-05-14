namespace BooksApi.Exceptions
{
  public class PasswordTooShortException : ApiException
  {
    public override string Title => "Validation Error";
    public override ErrorCode Code => ErrorCode.PASSWORD_TOO_SHORT;
    public override int StatusCode => 400;
    public PasswordTooShortException() : base($"Password must be at least {AuthConstants.MinPasswordLength} characters long.") { }
  }
}