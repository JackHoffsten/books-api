namespace BooksApi.Exceptions
{
  public abstract class ApiException : Exception
  {
    public abstract string Code { get; }
    public abstract int StatusCode { get; }

    protected ApiException(string message) : base(message) { }
  }
}