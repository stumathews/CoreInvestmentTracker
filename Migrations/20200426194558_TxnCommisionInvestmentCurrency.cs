using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreInvestmentTracker.Migrations
{
    public partial class TxnCommisionInvestmentCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Commission",
                table: "Transactions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Currency",
                table: "Investment",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Investment");
        }
    }
}
