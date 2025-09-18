using core.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.persistance.entityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(x => x.NameNormalized)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(x => x.Balance)
            .HasPrecision(18, 2)
            .IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

        }
    }
}
