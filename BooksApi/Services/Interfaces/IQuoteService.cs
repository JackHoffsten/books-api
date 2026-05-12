using BooksApi.DTOs.Quotes;

namespace BooksApi.Services.Interfaces
{
  public interface IQuoteService
  {
    Task<List<QuoteResponse>> GetQuotesByUserIdAsync(int userId);
    Task<QuoteResponse?> GetQuoteByIdAsync(int id);
    Task<QuoteResponse> CreateQuoteAsync(int userId, CreateQuoteRequest request);
    Task<QuoteResponse> UpdateQuoteAsync(int id, int userId, UpdateQuoteRequest request);
    Task DeleteQuoteAsync(int id, int userId);
  }
}
