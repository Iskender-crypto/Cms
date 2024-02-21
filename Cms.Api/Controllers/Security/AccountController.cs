using Cms.Domain.Entities;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers.Security;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AccountController(UserManager<IdentityUser> userManager, DataContext dataContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userName = HttpContext.User.Identity?.Name;
        if (userName == null) return new UnauthorizedResult();
        var user = await userManager.FindByNameAsync(userName);
        if (user == null) return new NotFoundResult();
        var profile = await dataContext.Set<Profile>().FirstOrDefaultAsync(p => p.UserId == user.Id);
        if (profile == null) throw new Exception();
        return Ok(profile);
    }

    [HttpPut]
    public Profile Update([FromBody] Profile model)
    {
        dataContext.Set<Profile>().Update(model);
        dataContext.SaveChanges();
        return model;
    }

    [HttpPut("password")]
    public async Task<IActionResult> Update([FromBody] string password)
    {
        var userName = HttpContext.User.Identity?.Name;
        if (userName == null) return new UnauthorizedResult();
        var user = await userManager.FindByNameAsync(userName);
        if (user == null) return new NotFoundResult();
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded)
        {
            throw new Exception();
        }

        return Ok();
    }
}