using BooksApi.Models;

namespace BooksApi.Repositories.Interfaces
{
  public interface IQuoteRepository
  {
    Task<List<Quote>> GetQuotesByUserIdAsync(int userId);
    Task<Quote?> GetQuoteByIdAsync(int id);
    Task<Quote> CreateQuoteAsync(Quote quote);
    Task<Quote> UpdateQuoteAsync(Quote quote);
    Task DeleteQuoteAsync(int id);
    Task SaveChangesAsync();
  }
}
