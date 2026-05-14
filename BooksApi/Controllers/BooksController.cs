using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BooksApi.DTOs.Books;
using BooksApi.Services.Interfaces;

namespace BooksApi.Controllers
{
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
      var userId = GetUserId();
      var books = await _bookService.GetBooksByUserIdAsync(userId);
      return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
      var userId = GetUserId();
      var book = await _bookService.GetBookByIdAsync(id);

      if (book == null || book.UserId != userId)
      {
        return NotFound(new { message = "Book not found" });
      }

      return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
      var userId = GetUserId();
      var book = await _bookService.CreateBookAsync(userId, request);
      return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookRequest request)
    {
      var userId = GetUserId();
      var book = await _bookService.UpdateBookAsync(id, userId, request);
      return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
      var userId = GetUserId();
      await _bookService.DeleteBookAsync(id, userId);
      return NoContent();
    }
  }
}
