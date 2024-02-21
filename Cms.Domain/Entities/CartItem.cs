namespace Cms.Domain.Entities;

public class CartItem : Entity
{
    public long CommodityId { get; set; }
    public int Count { get; set; }
    public long CartId { get; set; }
    public Commodity? Commodity { get; set; }
}