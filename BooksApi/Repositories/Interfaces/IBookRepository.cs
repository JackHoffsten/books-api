using BooksApi.Models;

namespace BooksApi.Repositories.Interfaces
{
  public interface IBookRepository
  {
    Task<List<Book>> GetBooksByUserIdAsync(int userId);
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task SaveChangesAsync();
  }
}
