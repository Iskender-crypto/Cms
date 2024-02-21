using Cms.Domain.Entities;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers.Public;

[ApiController]
[Route("[controller]")]
public class CartItemController(DataContext dataContext) : BaseController<CartItem>(dataContext)
{
    protected override IQueryable<CartItem> IncludePredicate(IQueryable<CartItem> items)
    {
        return base.IncludePredicate(items).Include(c=>c.Commodity);
    }
}