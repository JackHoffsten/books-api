using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BooksApi.DTOs.Books;
using BooksApi.Services.Interfaces;

namespace BooksApi.Controllers
{
  [ApiController]
  [Route("api/books")]
  [Authorize]
  public class BooksController : BaseController
  {
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
      _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
      try
      {
        var userId = GetUserId();
        var books = await _bookService.GetBooksByUserIdAsync(userId);
        return Ok(books);
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while retrieving books" });
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
      try
      {
        var userId = GetUserId();
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null || book.UserId != userId)
        {
          return NotFound(new { message = "Book not found" });
        }

        return Ok(book);
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while retrieving the book" });
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
      try
      {
        var userId = GetUserId();
        var book = await _bookService.CreateBookAsync(userId, request);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while creating the book" });
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookRequest request)
    {
      try
      {
        var userId = GetUserId();
        var book = await _bookService.UpdateBookAsync(id, userId, request);
        return Ok(book);
      }
      catch (InvalidOperationException ex)
      {
        return NotFound(new { message = ex.Message });
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while updating the book" });
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
      try
      {
        var userId = GetUserId();
        await _bookService.DeleteBookAsync(id, userId);
        return NoContent();
      }
      catch (InvalidOperationException ex)
      {
        return NotFound(new { message = ex.Message });
      }
      catch (Exception)
      {
        return StatusCode(500, new { message = "An error occurred while deleting the book" });
      }
    }
  }
}
