using Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Infrastructure.Ef.Configurations;

public class CommodityCategoryConfiguration : IEntityTypeConfiguration<CommodityCategory>
{
    public void Configure(EntityTypeBuilder<CommodityCategory> builder)
    {
        builder.ToTable("CommodityCategory");
    }
}