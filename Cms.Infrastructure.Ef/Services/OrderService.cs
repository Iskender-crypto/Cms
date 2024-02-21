using Cms.Domain.Entities;
using Cms.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Ef.Services;

public class OrderService(DataContext dataContext) : IOrderService
{
    public async Task<Order> Create(Order model, long cartId)
    {
        var cart = await dataContext.Set<Cart>()
            .Where(c => c.Id == cartId)
            .Include(c => c.CartItems)
            .ThenInclude(c => c.Commodity)
            .FirstOrDefaultAsync();

        model.OrderItems = cart.CartItems.Select(c =>
        {
            return new OrderItem()
            {
                OrderId = model.Id,
                Count = c.Count,
                Caption = c.Commodity.Caption,
                Description = c.Commodity.Description,
                WeightUnit = c.Commodity.WeightUnit,
                Weight = c.Commodity.Weight,
                ItemCount = c.Commodity.ItemCount,
                Price = c.Commodity.Price,
                FileId = c.Commodity.FileId,
            };
        }).ToList();
        dataContext.Set<Order>().Add(model);
        await dataContext.SaveChangesAsync();
        return model;
    }
}