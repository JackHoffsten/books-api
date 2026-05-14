namespace BooksApi.Exceptions
{
  public class UsernameEmptyException : ApiException
  {
    public override string Title => "Validation Error";
    public override ErrorCode Code => ErrorCode.USERNAME_EMPTY;
    public override int StatusCode => 400;
    public UsernameEmptyException() : base("Username cannot be empty.") { }
  }
}