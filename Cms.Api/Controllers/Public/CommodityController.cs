using Cms.Domain.Entities;
using Cms.Domain.Models;
using Cms.Domain.Services;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers.Public;
[ApiController]
[Route("[controller]")]
public class CommodityController(DataContext dataContext, ICommodityService commodityService) : BaseController<Commodity>(dataContext)
{
    protected override IQueryable<Commodity> IncludePredicate(IQueryable<Commodity> items)
    {
        return base.IncludePredicate(items)
            .Include(u => u.CommodityCategoryLinks);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =$"Administrator")]
    public override async Task<IActionResult> Add([FromBody] Commodity model)
    {
        var result = await commodityService.CreateAsync(model);
        return Ok(result);
    }

    protected override IQueryable<Commodity> FilterPredicate(Filter filter, IQueryable<Commodity> items)
    {
        switch (filter.Name)
        {
            case "caption": 
                return items.Where(c => 
                    EF.Functions.Like(c.Caption,$"%{filter.Value.GetString()}%"));
            case "description": 
                return items.Where(c =>
                    EF.Functions.Like(c.Description, $"%{filter.Value.GetString()}%"));
            case "price": 
                return items.Where(c => 
                    c.Price >= filter.Value.GetInt32());
            case "categoryId": 
                return items.Where(c =>
                    dataContext.Set<CommodityCategoryLink>().Any(l =>
                        l.Id == filter.Value.GetInt32() 
                        && l.CommodityCategoryId == filter.Value.GetInt32() 
                        && l.CommodityId == filter.Value.GetInt32()));
            default: return items;
        }
    }
}