using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Payment.API.Migrations
{
    public partial class Init_PaymentDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tid = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CusumBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    When = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BankSubAccId = table.Column<string>(type: "text", nullable: false),
                    SubAccId = table.Column<string>(type: "text", nullable: false),
                    BankName = table.Column<string>(type: "text", nullable: false),
                    BankAbbreviation = table.Column<string>(type: "text", nullable: false),
                    VirtualAccount = table.Column<string>(type: "text", nullable: true),
                    VirtualAccountName = table.Column<string>(type: "text", nullable: true),
                    CorresponsiveName = table.Column<string>(type: "text", nullable: false),
                    CorresponsiveAccount = table.Column<string>(type: "text", nullable: false),
                    CorresponsiveBankId = table.Column<string>(type: "text", nullable: false),
                    CorresponsiveBankName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
