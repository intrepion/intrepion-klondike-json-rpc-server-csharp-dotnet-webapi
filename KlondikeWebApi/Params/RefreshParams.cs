using System.Text.Json.Serialization;

namespace KlondikeWebApi.Params;

public class RefreshParams
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}
