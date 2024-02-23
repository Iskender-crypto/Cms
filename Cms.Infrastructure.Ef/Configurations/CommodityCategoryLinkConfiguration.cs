using Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Infrastructure.Ef.Configurations;

public class CommodityCategoryLinkConfiguration : IEntityTypeConfiguration<CommodityCategoryLink>
{
    public void Configure(EntityTypeBuilder<CommodityCategoryLink> builder)
    {
        builder.ToTable("CommodityCategoryLink");
    }
}