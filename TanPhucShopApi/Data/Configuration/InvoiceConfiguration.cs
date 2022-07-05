using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TanPhucShopApi.Models;

namespace TanPhucShopApi.Data.Configuration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Status).IsRequired();

        }
    }
}
