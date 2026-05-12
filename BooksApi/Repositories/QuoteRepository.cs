using Microsoft.EntityFrameworkCore;
using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Repositories.Interfaces;

namespace BooksApi.Repositories
{
  public class QuoteRepository : IQuoteRepository
  {
    private readonly BooksDbContext _context;

    public QuoteRepository(BooksDbContext context)
    {
      _context = context;
    }

    public async Task<List<Quote>> GetQuotesByUserIdAsync(int userId)
    {
      return await _context.Quotes
          .Where(q => q.UserId == userId)
          .ToListAsync();
    }

    public async Task<Quote?> GetQuoteByIdAsync(int id)
    {
      return await _context.Quotes.FindAsync(id);
    }

    public async Task<Quote> CreateQuoteAsync(Quote quote)
    {
      _context.Quotes.Add(quote);
      await _context.SaveChangesAsync();
      return quote;
    }

    public async Task<Quote> UpdateQuoteAsync(Quote quote)
    {
      _context.Quotes.Update(quote);
      await _context.SaveChangesAsync();
      return quote;
    }

    public async Task DeleteQuoteAsync(int id)
    {
      var quote = await _context.Quotes.FindAsync(id);
      if (quote != null)
      {
        _context.Quotes.Remove(quote);
        await _context.SaveChangesAsync();
      }
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
