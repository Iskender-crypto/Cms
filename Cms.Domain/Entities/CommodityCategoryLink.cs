namespace Cms.Domain.Entities;

public class CommodityCategoryLink : Entity
{
    public long CommodityId { get; set; }
    public long CommodityCategoryId { get; set; }
}