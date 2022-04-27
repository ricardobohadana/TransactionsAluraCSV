using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionsAluraCSV.Infra.Data.Migrations
{
    public partial class LogicalDeleteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SHOW",
                table: "USER",
                type: "boolean",
                nullable: true,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SHOW",
                table: "USER");
        }
    }
}
