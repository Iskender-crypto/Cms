using Cms.Domain.Models;
using Cms.Domain.Utils;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers.Security;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"Administrator")]
public class RoleController(RoleManager<IdentityRole> roleManager, DataContext dataContext) : ControllerBase
{
    protected virtual IQueryable<IdentityRole> FilterPredicate(Filter filter, IQueryable<IdentityRole> items)
    {
        switch (filter.Name)
        {
            case "name":
                return items.Where(r =>
                    EF.Functions.Like(r.NormalizedName, $"%{filter.Value.GetString()!.ToUpper()}%"));
            default: return items;
        }
    }

    [HttpGet]
    public async Task<ListResponse<IdentityRole>> GetListAsync(string? filter, string? orderField, string? orderType,
        int? pageIndex = 1, int? pageSize = 20)
    {
        var query = dataContext.Roles.Filter(filter ?? "", FilterPredicate)
            .Sort(orderField ?? "Id", orderType ?? "ASC");
        var total = await query.CountAsync();
        var items = await query
            .Paginate(pageIndex, pageSize).ToListAsync();
        return new ListResponse<IdentityRole>(total, items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var model = await roleManager.FindByIdAsync(id);
        if (model == null) throw new NullReferenceException();
        return Ok(model);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Add([FromBody] IdentityRole model)
    {
        await roleManager.CreateAsync(model);
        return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] IdentityRole model)
    {
        await roleManager.UpdateAsync(model);
        return Ok(model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) throw new NullReferenceException();
            await roleManager.DeleteAsync(role);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}