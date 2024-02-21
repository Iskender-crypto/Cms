using System.Linq.Expressions;
using Cms.Domain.Entities;
using Cms.Domain.Models;
using Cms.Domain.Services;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers.Public;

[ApiController]
[Route("[controller]")]
public class OrderController(DataContext dataContext, IOrderService orderService, ICartService cartService) : BaseController<Order>(dataContext)
{
    protected override Order GetModel(Expression<Func<Order, bool>> expression)
    {
        var model = dataContext.Set<Order>()
            .Include(o => o.OrderItems)
            .SingleOrDefault(expression);
        if (model == null) throw new NullReferenceException();
        return model;
    }

    protected override IQueryable<Order> FilterPredicate(Filter filter, IQueryable<Order> items)
    {
        switch (filter.Name)
        {
            case "fullName": return items.Where(o => EF.Functions.Like(o.FullName, $"%{filter.Value.GetString()}%"));
            case "address": return items.Where(o => EF.Functions.Like(o.Address, $"%{filter.Value.GetString()}%"));
            case "phone": return items.Where(o => EF.Functions.Like(o.Phone, $"%{filter.Value.GetString()}%"));
            default: return items;
        }
    }

    [HttpPost]
    public override async Task<IActionResult> Add([FromBody] Order model)
    {
        long cartId = cartService.GetCartIdFromRequest();
        await orderService.Create(model, cartId);
        return Ok(model);
    }
}