namespace BooksApi.DTOs.Books
{
  public class CreateBookRequest
  {
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string? Description { get; set; }
  }
}
