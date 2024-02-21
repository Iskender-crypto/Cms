namespace Cms.Domain.Entities;

public class Cart : Entity
{
    public string Uid { get; set; }
    public List<CartItem> CartItems { get; set; }
}