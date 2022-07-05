﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TanPhucShopApi.Models;

namespace TanPhucShopApi.Data.Configuration
{
    public class RoleConfiguration: IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(15);
            builder.Property(x => x.Description).HasMaxLength(100);
        }

    }
}