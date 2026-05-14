namespace BooksApi.Exceptions
{
  public class UsernameTakenException : ApiException
  {
    public override string Title => "Validation Error";
    public override ErrorCode Code => ErrorCode.USERNAME_TAKEN;
    public override int StatusCode => 409;
    public UsernameTakenException() : base("Username is already taken.") { }
  }
}