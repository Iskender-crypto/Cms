using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Cms.Domain.Entities;

public class Profile : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserId { get; set; }
    [JsonIgnore] public IdentityUser? User { get; set; }
}