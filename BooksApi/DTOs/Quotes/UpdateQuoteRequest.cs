namespace BooksApi.DTOs.Quotes
{
  public class UpdateQuoteRequest
  {
    public string Text { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
  }
}
