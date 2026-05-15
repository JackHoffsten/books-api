using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BooksApi.DTOs.Quotes;
using BooksApi.Services.Interfaces;

namespace BooksApi.Controllers
{
  [Route("api/quotes")]
  [Authorize]
  public class QuotesController : BaseController
  {
    private readonly IQuoteService _quoteService;

    public QuotesController(IQuoteService quoteService)
    {
      _quoteService = quoteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuotes()
    {
      var userId = GetUserId();
      var quotes = await _quoteService.GetQuotesByUserIdAsync(userId);

      return Ok(quotes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuote(int id)
    {
      var userId = GetUserId();
      var quote = await _quoteService.GetQuoteByIdAsync(id, userId);

      return Ok(quote);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteRequest request)
    {
      var userId = GetUserId();
      var quote = await _quoteService.CreateQuoteAsync(userId, request);

      return CreatedAtAction(nameof(GetQuote), new { id = quote.Id }, quote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuote(int id, [FromBody] UpdateQuoteRequest request)
    {
      var userId = GetUserId();
      var quote = await _quoteService.UpdateQuoteAsync(id, userId, request);

      return Ok(quote);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuote(int id)
    {
      var userId = GetUserId();
      await _quoteService.DeleteQuoteAsync(id, userId);

      return NoContent();
    }
  }
}
