namespace BooksApi.Exceptions
{
  public class EmailEmptyException : ApiException
  {
    public override string Title => "Validation Error";
    public override ErrorCode Code => ErrorCode.EMAIL_EMPTY;
    public override int StatusCode => 400;
    public EmailEmptyException() : base("Email cannot be empty.") { }
  }
}