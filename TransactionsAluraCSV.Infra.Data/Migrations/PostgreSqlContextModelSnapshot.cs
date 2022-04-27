﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TransactionsAluraCSV.Infra.Data.Contexts;

#nullable disable

namespace TransactionsAluraCSV.Infra.Data.Migrations
{
    [DbContext(typeof(PostgreSqlContext))]
    partial class PostgreSqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TransactionsAluraCSV.Domain.Entities.Transfer", b =>
                {
                    b.Property<Guid>("TransferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("TRANSFERID");

                    b.Property<string>("DestinationAccount")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("DESTINATIONACCOUNT");

                    b.Property<string>("DestinationAgency")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)")
                        .HasColumnName("DESTINATIONAGENCY");

                    b.Property<string>("DestinationBank")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("DESTINATIONBANK");

                    b.Property<string>("OriginAccount")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("ORIGINACCOUNT");

                    b.Property<string>("OriginAgency")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)")
                        .HasColumnName("ORIGINAGENCY");

                    b.Property<string>("OriginBank")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("ORIGINBANK");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("REGISTERDATE");

                    b.Property<decimal>("TransferAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("TRANSFERAMOUNT");

                    b.Property<DateTime>("TransferDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("TRANSFERDATE");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("USERID");

                    b.HasKey("TransferId");

                    b.HasIndex("UserId");

                    b.ToTable("TRANSFER", (string)null);
                });

            modelBuilder.Entity("TransactionsAluraCSV.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("USERID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("NAME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)")
                        .HasColumnName("PASSWORD");

                    b.Property<bool?>("show")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("SHOW");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("USER", (string)null);
                });

            modelBuilder.Entity("TransactionsAluraCSV.Domain.Entities.Transfer", b =>
                {
                    b.HasOne("TransactionsAluraCSV.Domain.Entities.User", "User")
                        .WithMany("Transfers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TransactionsAluraCSV.Domain.Entities.User", b =>
                {
                    b.Navigation("Transfers");
                });
#pragma warning restore 612, 618
        }
    }
}
