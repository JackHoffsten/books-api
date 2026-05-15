namespace BooksApi.Exceptions.Quote
{
  public class QuoteNotFoundException : ApiException
  {
    public override string Title => "Not Found";
    public override ErrorCode Code => ErrorCode.QUOTE_NOT_FOUND;
    public override int StatusCode => 404;
    public QuoteNotFoundException() : base("The requested quote was not found.") { }
  }
}