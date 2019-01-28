using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreInvestmentTracker.Migrations
{
    public partial class AddPointsAndFlaggedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "Users",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "Risks",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "Regions",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "RecordedActivities",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "Notes",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "InvestmentInfluenceFactor",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "Investment",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "Groups",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "CustomEntityTypes",
                newName: "IsFlagged");

            migrationBuilder.RenameColumn(
                name: "IsFlaged",
                table: "CustomEntities",
                newName: "IsFlagged");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "Users",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "Risks",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "Regions",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "RecordedActivities",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "Notes",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "InvestmentInfluenceFactor",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "Investment",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "Groups",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "CustomEntityTypes",
                newName: "IsFlaged");

            migrationBuilder.RenameColumn(
                name: "IsFlagged",
                table: "CustomEntities",
                newName: "IsFlaged");
        }
    }
}
