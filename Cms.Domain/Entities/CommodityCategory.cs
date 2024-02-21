namespace Cms.Domain.Entities;

public class CommodityCategory : Entity
{
    public string Caption { get; set; }    
    public List<CommodityCategoryLink> CommodityCategoryLinks { get; set; } = new List<CommodityCategoryLink>();

}