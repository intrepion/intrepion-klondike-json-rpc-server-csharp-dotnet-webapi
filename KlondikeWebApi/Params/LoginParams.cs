using System.Text.Json.Serialization;

namespace KlondikeWebApi.Params;

public class LoginParams
{
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("username")]
    public string? UserName { get; set; }
}
