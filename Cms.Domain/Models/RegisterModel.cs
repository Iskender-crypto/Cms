using Cms.Domain.Entities;

namespace Cms.Domain.Models;

public class RegisterModel{
    public string Login { get; set; }    
    public string Password { get; set; }
    public Profile Profile { get; set; }
}