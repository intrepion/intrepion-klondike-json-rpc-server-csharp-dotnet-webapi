using System.Text.Json.Serialization;

namespace KlondikeWebApi.Results;

public class RefreshResult
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}
