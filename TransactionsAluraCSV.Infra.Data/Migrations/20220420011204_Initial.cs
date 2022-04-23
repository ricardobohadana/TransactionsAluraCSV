using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionsAluraCSV.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    USERID = table.Column<Guid>(type: "uuid", nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    NAME = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.USERID);
                });

            migrationBuilder.CreateTable(
                name: "TRANSFER",
                columns: table => new
                {
                    TRANSFERID = table.Column<Guid>(type: "uuid", nullable: false),
                    USERID = table.Column<Guid>(type: "uuid", nullable: false),
                    ORIGINBANK = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ORIGINAGENCY = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    ORIGINACCOUNT = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    DESTINATIONBANK = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DESTINATIONAGENCY = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    DESTINATIONACCOUNT = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    TRANSFERAMOUNT = table.Column<decimal>(type: "numeric", nullable: false),
                    TRANSFERDATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSFER", x => x.TRANSFERID);
                    table.ForeignKey(
                        name: "FK_TRANSFER_USER_USERID",
                        column: x => x.USERID,
                        principalTable: "USER",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRANSFER_USERID",
                table: "TRANSFER",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_EMAIL",
                table: "USER",
                column: "EMAIL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRANSFER");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
