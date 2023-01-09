using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace KlondikeWebApi.Entities;

public class UserEntity : IdentityUser<Guid>
{
    [JsonPropertyName("guid")]
    public Guid? Guid { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("refresh_token_expiry_time")]
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
