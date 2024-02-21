namespace Cms.Domain.Entities;

public class OrderItem : Entity
{
    public string Caption { get; set; }
    public string Description { get; set; }
    public double Weight { get; set; }
    public string WeightUnit { get; set; }
    public int ItemCount { get; set; }
    public int Price { get; set; }
    public long FileId { get; set; }
    public File? File { get; set; }
    public Commodity? Commodity { get; set; }
    public int Count { get; set; }
    public long OrderId { get; set; }
}