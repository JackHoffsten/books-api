namespace BooksApi.Exceptions
{
  public class EmailTakenException : ApiException
  {
    public override string Code => "EMAIL_TAKEN";
    public override int StatusCode => 400;
    public EmailTakenException() : base("Email is already registered.") { }
  }
}