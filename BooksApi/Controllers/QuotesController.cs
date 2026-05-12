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
      try
      {
        var userId = GetUserId();
        var quotes = await _quoteService.GetQuotesByUserIdAsync(userId);
        return Ok(quotes);
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while retrieving quotes" });
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuote(int id)
    {
      try
      {
        var userId = GetUserId();
        var quote = await _quoteService.GetQuoteByIdAsync(id);

        if (quote == null || quote.UserId != userId)
        {
          return NotFound(new { message = "Quote not found" });
        }

        return Ok(quote);
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while retrieving the quote" });
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteRequest request)
    {
      try
      {
        var userId = GetUserId();
        var quote = await _quoteService.CreateQuoteAsync(userId, request);
        return CreatedAtAction(nameof(GetQuote), new { id = quote.Id }, quote);
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while creating the quote" });
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuote(int id, [FromBody] UpdateQuoteRequest request)
    {
      try
      {
        var userId = GetUserId();
        var quote = await _quoteService.UpdateQuoteAsync(id, userId, request);
        return Ok(quote);
      }
      catch (InvalidOperationException ex)
      {
        return NotFound(new { message = ex.Message });
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while updating the quote" });
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuote(int id)
    {
      try
      {
        var userId = GetUserId();
        await _quoteService.DeleteQuoteAsync(id, userId);
        return NoContent();
      }
      catch (InvalidOperationException ex)
      {
        return NotFound(new { message = ex.Message });
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while deleting the quote" });
      }
    }
  }
}
