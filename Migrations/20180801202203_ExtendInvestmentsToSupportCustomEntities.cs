using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreInvestmentTracker.Migrations
{
    public partial class ExtendInvestmentsToSupportCustomEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomEntity_Investment",
                columns: table => new
                {
                    InvestmentID = table.Column<int>(nullable: false),
                    CustomEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InvestmentID_CustomEntityID", x => new { x.InvestmentID, x.CustomEntityId });
                    table.ForeignKey(
                        name: "FK_CustomEntity_Investment_CustomEntities_CustomEntityId",
                        column: x => x.CustomEntityId,
                        principalTable: "CustomEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomEntity_Investment_Investment_InvestmentID",
                        column: x => x.InvestmentID,
                        principalTable: "Investment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomEntity_Investment_CustomEntityId",
                table: "CustomEntity_Investment",
                column: "CustomEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomEntity_Investment");
        }
    }
}
