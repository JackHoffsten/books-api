using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorCode
{
  USERNAME_EMPTY,

  EMAIL_EMPTY,

  USERNAME_TAKEN,

  EMAIL_TAKEN,

  PASSWORD_TOO_SHORT,

  INVALID_CREDENTIALS,
}