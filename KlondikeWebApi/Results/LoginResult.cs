using System.Text.Json.Serialization;

namespace KlondikeWebApi.Results;

public class LoginResult
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("user")]
    public LoginResultUser? User { get; set; }
}
