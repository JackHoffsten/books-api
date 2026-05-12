using BooksApi.DTOs.Books;

namespace BooksApi.Services.Interfaces
{
  public interface IBookService
  {
    Task<List<BookResponse>> GetBooksByUserIdAsync(int userId);
    Task<BookResponse?> GetBookByIdAsync(int id);
    Task<BookResponse> CreateBookAsync(int userId, CreateBookRequest request);
    Task<BookResponse> UpdateBookAsync(int id, int userId, UpdateBookRequest request);
    Task DeleteBookAsync(int id, int userId);
  }
}
