using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoreInvestmentTracker.Migrations
{
    public partial class AddTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFactor_InvestmentInfluenceFactorID",
                table: "InvestmentInfluenceFactor_Investment");

            migrationBuilder.RenameIndex(
                name: "IX_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFactorID",
                table: "InvestmentInfluenceFactor_Investment",
                newName: "IX_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFac~");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Points = table.Column<long>(nullable: false),
                    IsFlagged = table.Column<bool>(nullable: false),
                    InvestmentId = table.Column<int>(nullable: false),
                    NumUnits = table.Column<int>(nullable: false),
                    PricePerUnit = table.Column<float>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    TransactionType = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_Id", x => x.Id);
                    table.UniqueConstraint("AK_Transactions_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Transactions_Investment_InvestmentId",
                        column: x => x.InvestmentId,
                        principalTable: "Investment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InvestmentId",
                table: "Transactions",
                column: "InvestmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFac~",
                table: "InvestmentInfluenceFactor_Investment",
                column: "InvestmentInfluenceFactorID",
                principalTable: "InvestmentInfluenceFactor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFac~",
                table: "InvestmentInfluenceFactor_Investment");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFac~",
                table: "InvestmentInfluenceFactor_Investment",
                newName: "IX_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFactorID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFactor_InvestmentInfluenceFactorID",
                table: "InvestmentInfluenceFactor_Investment",
                column: "InvestmentInfluenceFactorID",
                principalTable: "InvestmentInfluenceFactor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
