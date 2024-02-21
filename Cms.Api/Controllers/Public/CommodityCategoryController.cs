using Cms.Domain.Entities;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers.Public;

[ApiController]
[Route("[controller]")]
public class CommodityCategoryController : BaseController<CommodityCategory>
{
    public CommodityCategoryController(DataContext dataContext) : base(dataContext)
    {
    }

    protected override IQueryable<CommodityCategory> IncludePredicate(IQueryable<CommodityCategory> items)
    {
        return base.IncludePredicate(items).Include(c=>c.CommodityCategoryLinks);
    }
}