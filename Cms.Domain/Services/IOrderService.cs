using Cms.Domain.Entities;

namespace Cms.Domain.Services;

public interface IOrderService
{
    public Task<Order> Create(Order order, long cartId);
}