namespace BooksApi.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<Book> Books { get; set; } = new List<Book>();
    public List<Quote> Quotes { get; set; } = new List<Quote>();
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
  }
}