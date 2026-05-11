namespace BooksApi.DTOs.Quotes
{
  public class QuoteResponse
  {
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
  }
}
