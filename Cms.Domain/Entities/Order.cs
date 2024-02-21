namespace Cms.Domain.Entities;

public class Order:Entity
{
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}