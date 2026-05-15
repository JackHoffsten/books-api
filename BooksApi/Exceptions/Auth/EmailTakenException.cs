namespace BooksApi.Exceptions.Auth
{
  public class EmailTakenException : ApiException
  {
    public override string Title => "Validation Error";
    public override ErrorCode Code => ErrorCode.EMAIL_TAKEN;
    public override int StatusCode => 409;
    public EmailTakenException() : base("Email is already registered.") { }
  }
}