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
    public class TransferMap : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {

            builder.ToTable("TRANSFER");

            builder.HasKey(transfer => transfer.TransferId);

            builder.Property(transfer => transfer.TransferId).HasColumnName("TRANSFERID");

            builder.HasOne(transfer => transfer.User).WithMany(user => user.Transfers).HasForeignKey(transfer => transfer.UserId);
            builder.Property(transfer => transfer.UserId).HasColumnName("USERID");

            builder.Property(transfer => transfer.OriginBank).HasColumnName("ORIGINBANK").HasMaxLength(100).IsRequired();
            builder.Property(transfer => transfer.OriginAgency).HasColumnName("ORIGINAGENCY").HasMaxLength(4).IsRequired();
            builder.Property(transfer => transfer.OriginAccount).HasColumnName("ORIGINACCOUNT").HasMaxLength(7).IsRequired();

            builder.Property(transfer => transfer.DestinationBank).HasColumnName("DESTINATIONBANK").HasMaxLength(100).IsRequired();
            builder.Property(transfer => transfer.DestinationAgency).HasColumnName("DESTINATIONAGENCY").HasMaxLength(4).IsRequired();
            builder.Property(transfer => transfer.DestinationAccount).HasColumnName("DESTINATIONACCOUNT").HasMaxLength(7).IsRequired();

            builder.Property(transfer => transfer.TransferAmount).HasColumnName("TRANSFERAMOUNT").IsRequired();
            builder.Property(transfer => transfer.TransferDate).HasColumnName("TRANSFERDATE").IsRequired();

            builder.Property(transfer => transfer.RegisterDate).HasColumnName("REGISTERDATE").IsRequired();
        }
    }
}
