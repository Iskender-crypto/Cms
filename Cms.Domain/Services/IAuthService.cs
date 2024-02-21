using Cms.Domain.Models;

namespace Cms.Domain.Services;

public interface IAuthService
{
    public Task<LoginModel> Login(AuthModel model);
    public Task<LoginModel> Register(RegisterModel model);
}