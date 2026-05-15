using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorCode
{
  UNAUTHORIZED,
  USERNAME_EMPTY,
  EMAIL_EMPTY,
  USERNAME_TAKEN,
  EMAIL_TAKEN,
  PASSWORD_TOO_SHORT,
  INVALID_CREDENTIALS,

  BOOK_NOT_FOUND,

  QUOTE_NOT_FOUND,

  INTERNAL_SERVER_ERROR,
}