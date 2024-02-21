namespace Cms.Domain.Entities;

public abstract class Entity
{
    public long Id { get; set; }
    public string? SystemName { get; set; }
}