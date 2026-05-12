using Microsoft.EntityFrameworkCore;
using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Repositories.Interfaces;

namespace BooksApi.Repositories
{
  public class BookRepository : IBookRepository
  {
    private readonly BooksDbContext _context;

    public BookRepository(BooksDbContext context)
    {
      _context = context;
    }

    public async Task<List<Book>> GetBooksByUserIdAsync(int userId)
    {
      return await _context.Books
          .Where(b => b.UserId == userId)
          .ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
      return await _context.Books.FindAsync(id);
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
      _context.Books.Add(book);
      await _context.SaveChangesAsync();
      return book;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
      _context.Books.Update(book);
      await _context.SaveChangesAsync();
      return book;
    }

    public async Task DeleteBookAsync(int id)
    {
      var book = await _context.Books.FindAsync(id);
      if (book != null)
      {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
      }
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
