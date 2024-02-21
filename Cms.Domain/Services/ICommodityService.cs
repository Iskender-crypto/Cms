using Cms.Domain.Entities;

namespace Cms.Domain.Services;

public interface ICommodityService
{
    public Task<Commodity> CreateAsync(Commodity model);
}