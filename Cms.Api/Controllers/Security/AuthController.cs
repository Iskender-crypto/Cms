using Cms.Domain.Models;
using Cms.Domain.Services;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers.Security;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService, DataContext dataContext) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthModel model)
    {
        var result = await authService.Login(model);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await authService.Register(model);
        return Ok(result);
    }
}