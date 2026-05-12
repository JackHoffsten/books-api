using BooksApi.DTOs.Books;
using BooksApi.Models;
using BooksApi.Repositories.Interfaces;
using BooksApi.Services.Interfaces;

namespace BooksApi.Services
{
  public class BookService : IBookService
  {
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
      _bookRepository = bookRepository;
    }

    public async Task<List<BookResponse>> GetBooksByUserIdAsync(int userId)
    {
      var books = await _bookRepository.GetBooksByUserIdAsync(userId);
      return books.Select(MapToResponse).ToList();
    }

    public async Task<BookResponse?> GetBookByIdAsync(int id)
    {
      var book = await _bookRepository.GetBookByIdAsync(id);
      return book != null ? MapToResponse(book) : null;
    }

    public async Task<BookResponse> CreateBookAsync(int userId, CreateBookRequest request)
    {
      var book = new Book
      {
        Title = request.Title,
        Author = request.Author,
        PublishedDate = request.PublishedDate,
        Description = request.Description,
        UserId = userId,
        CreatedAt = DateTime.UtcNow
      };

      var createdBook = await _bookRepository.CreateBookAsync(book);
      return MapToResponse(createdBook);
    }

    public async Task<BookResponse> UpdateBookAsync(int id, int userId, UpdateBookRequest request)
    {
      var book = await _bookRepository.GetBookByIdAsync(id);
      if (book == null || book.UserId != userId)
      {
        throw new InvalidOperationException("Book not found or unauthorized");
      }

      book.Title = request.Title;
      book.Author = request.Author;
      book.PublishedDate = request.PublishedDate;
      book.Description = request.Description;

      var updatedBook = await _bookRepository.UpdateBookAsync(book);
      return MapToResponse(updatedBook);
    }

    public async Task DeleteBookAsync(int id, int userId)
    {
      var book = await _bookRepository.GetBookByIdAsync(id);
      if (book == null || book.UserId != userId)
      {
        throw new InvalidOperationException("Book not found or unauthorized");
      }

      await _bookRepository.DeleteBookAsync(id);
    }

    private BookResponse MapToResponse(Book book)
    {
      return new BookResponse
      {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        PublishedDate = book.PublishedDate,
        Description = book.Description,
        CreatedAt = book.CreatedAt,
        UserId = book.UserId
      };
    }
  }
}
