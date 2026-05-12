using BooksApi.DTOs.Quotes;
using BooksApi.Models;
using BooksApi.Repositories.Interfaces;
using BooksApi.Services.Interfaces;

namespace BooksApi.Services
{
  public class QuoteService : IQuoteService
  {
    private readonly IQuoteRepository _quoteRepository;

    public QuoteService(IQuoteRepository quoteRepository)
    {
      _quoteRepository = quoteRepository;
    }

    public async Task<List<QuoteResponse>> GetQuotesByUserIdAsync(int userId)
    {
      var quotes = await _quoteRepository.GetQuotesByUserIdAsync(userId);
      return quotes.Select(MapToResponse).ToList();
    }

    public async Task<QuoteResponse?> GetQuoteByIdAsync(int id)
    {
      var quote = await _quoteRepository.GetQuoteByIdAsync(id);
      return quote != null ? MapToResponse(quote) : null;
    }

    public async Task<QuoteResponse> CreateQuoteAsync(int userId, CreateQuoteRequest request)
    {
      var quote = new Quote
      {
        Text = request.Text,
        Author = request.Author,
        UserId = userId,
        CreatedAt = DateTime.UtcNow
      };

      var createdQuote = await _quoteRepository.CreateQuoteAsync(quote);
      return MapToResponse(createdQuote);
    }

    public async Task<QuoteResponse> UpdateQuoteAsync(int id, int userId, UpdateQuoteRequest request)
    {
      var quote = await _quoteRepository.GetQuoteByIdAsync(id);
      if (quote == null || quote.UserId != userId)
      {
        throw new InvalidOperationException("Quote not found or unauthorized");
      }

      quote.Text = request.Text;
      quote.Author = request.Author;

      var updatedQuote = await _quoteRepository.UpdateQuoteAsync(quote);
      return MapToResponse(updatedQuote);
    }

    public async Task DeleteQuoteAsync(int id, int userId)
    {
      var quote = await _quoteRepository.GetQuoteByIdAsync(id);
      if (quote == null || quote.UserId != userId)
      {
        throw new InvalidOperationException("Quote not found or unauthorized");
      }

      await _quoteRepository.DeleteQuoteAsync(id);
    }

    private QuoteResponse MapToResponse(Quote quote)
    {
      return new QuoteResponse
      {
        Id = quote.Id,
        Text = quote.Text,
        Author = quote.Author,
        CreatedAt = quote.CreatedAt,
        UserId = quote.UserId
      };
    }
  }
}
