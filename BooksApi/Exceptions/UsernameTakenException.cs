namespace BooksApi.Exceptions
{
  public class UsernameTakenException : ApiException
  {
    public override string Code => "USERNAME_TAKEN";
    public override int StatusCode => 400;
    public UsernameTakenException() : base("Username is already taken.") { }
  }
}