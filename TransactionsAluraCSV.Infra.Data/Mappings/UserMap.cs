using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;

namespace TransactionsAluraCSV.Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");

            builder.HasKey(user => user.UserId);

            builder.Property(user => user.UserId).HasColumnName("USERID");
            builder.Property(user => user.show).HasColumnName("SHOW").HasDefaultValue(true);
            builder.Property(user => user.Name).HasColumnName("NAME").HasMaxLength(150).IsRequired();
            builder.Property(user => user.Email).HasColumnName("EMAIL").HasMaxLength(100).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.Password).HasColumnName("PASSWORD").HasMaxLength(70).IsRequired();
        }
    }
}
