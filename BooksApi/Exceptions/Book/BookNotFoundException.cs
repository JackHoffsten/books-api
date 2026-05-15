namespace BooksApi.Exceptions.Book
{
  public class BookNotFoundException : ApiException
  {
    public override string Title => "Not Found";
    public override ErrorCode Code => ErrorCode.BOOK_NOT_FOUND;
    public override int StatusCode => 404;
    public BookNotFoundException() : base("The requested book was not found.") { }
  }
}