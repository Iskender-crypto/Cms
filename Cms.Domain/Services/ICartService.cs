using Cms.Domain.Entities;

namespace Cms.Domain.Services;

public interface ICartService
{
    public Task<Cart> GetCart();
    public long GetCartIdFromRequest();
}