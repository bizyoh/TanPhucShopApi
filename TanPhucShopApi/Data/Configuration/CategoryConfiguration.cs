using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TanPhucShopApi.Models;

namespace TanPhucShopApi.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category>builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired();

        }
    }
}
