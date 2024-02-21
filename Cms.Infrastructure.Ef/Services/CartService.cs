using Cms.Domain.Entities;
using Cms.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Ef.Services;

public class CartService(DataContext dataContext, IHttpContextAccessor accessor, UidService uidService) : ICartService
{
    public async Task<Cart> GetCart()
    {
        var uid = uidService.GetUid().ToString();
        var cart = await dataContext.Set<Cart>()
            .Include(cart => cart.CartItems)
            .ThenInclude(c => c.Commodity)
            .ThenInclude(c => c.CommodityCategoryLinks)
            .FirstOrDefaultAsync(x => x.Uid == uid);

        if (cart != null) return cart;

        cart = new Cart
        {
            Uid = uid
        };
        dataContext.Set<Cart>().Add(cart);
        await dataContext.SaveChangesAsync();

        return cart;
    }

    public long GetCartIdFromRequest()
    {
        var context = accessor.HttpContext;
        if (context == null) throw new NullReferenceException();
        var cartId = context.Request.Query["cartId"].ToString();
        if (cartId == null) throw new NullReferenceException("cartId не передан");
        var isSuccess = long.TryParse(cartId, out var result);
        if (!isSuccess) throw new Exception("Аргумент не верный cartId");
        return result;
    }
}