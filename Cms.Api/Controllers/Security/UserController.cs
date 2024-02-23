using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers.Security;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"Administrator")]
public class UserController : ControllerBase
{
    // HttpGet GetList(...params)
    // HttpPost Add(RegisterModel model)
    // HttpPut Update(Profile model)
    // HttpDelete("{id}") Delete(long id)
    //... BaseController
    // dataContext.Users
    // HttpPost("{id}/role") AddRole(long id, [FromBody] List<string> roles)
    // HttpDelete("{id}/role") DeleteRole (long id, [FromBody] List<string> roles)
    // HttpPut("{id}/password")
}