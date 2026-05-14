namespace BooksApi.Exceptions
{
  public class InvalidCredentialsException : ApiException
  {
    public override string Code => "INVALID_CREDENTIALS";
    public override int StatusCode => 400;
    public InvalidCredentialsException() : base("Invalid username or password.") { }
  }
}