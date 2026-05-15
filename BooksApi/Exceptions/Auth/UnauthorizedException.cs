namespace BooksApi.Exceptions.Auth
{
  public class UnauthorizedException : ApiException
  {
    public override string Title => "Unauthorized";
    public override ErrorCode Code => ErrorCode.UNAUTHORIZED;
    public override int StatusCode => 401;
    public UnauthorizedException() : base("You are not authorized to access this resource.") { }
  }
}