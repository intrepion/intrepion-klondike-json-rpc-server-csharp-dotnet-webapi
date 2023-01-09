using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace KlondikeWebApi.Entities;

public class RoleEntity : IdentityRole<Guid>
{
    [JsonPropertyName("guid")]
    public Guid? Guid { get; set; }
}
