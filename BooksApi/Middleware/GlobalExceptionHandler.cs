using System.Text.Json;
using BooksApi.Exceptions.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Middleware
{
  public class GlobalExceptionHandler
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

      var problemDetails = new ProblemDetails();

      switch (exception)
      {
        case UsernameEmptyException ex:
          problemDetails = new ProblemDetails
          {
            Title = ex.Title,
            Detail = ex.Message,
            Status = ex.StatusCode,
            Extensions =
            {
              ["code"] = ex.Code,
              ["timestamp"] = DateTime.UtcNow
            }
          };
          context.Response.StatusCode = ex.StatusCode;
          break;
        case EmailEmptyException ex:
          problemDetails = new ProblemDetails
          {
            Title = ex.Title,
            Detail = ex.Message,
            Status = ex.StatusCode,
            Extensions =
            {
              ["code"] = ex.Code,
              ["timestamp"] = DateTime.UtcNow
            }
          };
          context.Response.StatusCode = ex.StatusCode;
          break;
        case UsernameTakenException ex:
          problemDetails = new ProblemDetails
          {
            Title = ex.Title,
            Detail = ex.Message,
            Status = ex.StatusCode,
            Extensions =
            {
              ["code"] = ex.Code,
              ["timestamp"] = DateTime.UtcNow
            }
          };
          context.Response.StatusCode = ex.StatusCode;
          break;
        case EmailTakenException ex:
          problemDetails = new ProblemDetails
          {
            Title = ex.Title,
            Detail = ex.Message,
            Status = ex.StatusCode,
            Extensions =
            {
              ["code"] = ex.Code,
              ["timestamp"] = DateTime.UtcNow
            }
          };
          context.Response.StatusCode = ex.StatusCode;
          break;
        case InvalidCredentialsException ex:
          problemDetails = new ProblemDetails
          {
            Title = ex.Title,
            Detail = ex.Message,
            Status = ex.StatusCode,
            Extensions =
            {
              ["code"] = ex.Code,
              ["timestamp"] = DateTime.UtcNow
            }
          };
          context.Response.StatusCode = ex.StatusCode;
          break;
        default:
          problemDetails = new ProblemDetails
          {
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred. Please try again later.",
            Status = 500,
            Extensions =
            {
              ["code"] = ErrorCode.INTERNAL_SERVER_ERROR,
              ["timestamp"] = DateTime.UtcNow
            }
          };
          context.Response.StatusCode = 500;
          break;
      }

      context.Response.ContentType = "application/json";
      var json = JsonSerializer.Serialize(problemDetails);
      await context.Response.WriteAsync(json);
    }
  }
}