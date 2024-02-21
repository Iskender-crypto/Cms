using Cms.Domain.Entities;

namespace Cms.Domain.Models;

public class LoginModel
{
    public string Token { get; set; }
    public Profile Profile { get; set; }
    public IList<string> Roles { get; set; }
}