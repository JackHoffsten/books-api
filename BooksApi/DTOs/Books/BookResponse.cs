namespace BooksApi.DTOs.Books
{
  public class BookResponse
  {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
  }
}
