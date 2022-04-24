using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionsAluraCSV.Infra.Data.Migrations
{
    public partial class TransferFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "REGISTERDATE",
                table: "TRANSFER",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "REGISTERDATE",
                table: "TRANSFER");
        }
    }
}
