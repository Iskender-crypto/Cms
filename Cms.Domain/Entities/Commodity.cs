namespace Cms.Domain.Entities;

public class Commodity : Entity
{
    public string Caption { get; set; }
    public string Description { get; set; }
    public double Weight { get; set; }
    public string WeightUnit { get; set; }
    public int ItemCount { get; set; }
    public int Price { get; set; }
    
    public long FileId { get; set; }
    
    public File? File { get; set; }
    public List<CommodityCategoryLink> CommodityCategoryLinks { get; set; } = new List<CommodityCategoryLink>();
}