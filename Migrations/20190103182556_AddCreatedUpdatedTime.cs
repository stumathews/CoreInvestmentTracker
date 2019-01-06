using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreInvestmentTracker.Migrations
{
    public partial class AddCreatedUpdatedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AtTime",
                table: "RecordedActivities",
                newName: "LastModifiedTime");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "Users",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "Users",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "Risks",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "Risks",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "Regions",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "Regions",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "RecordedActivities",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "Notes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "Notes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "InvestmentInfluenceFactor",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "InvestmentInfluenceFactor",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "Investment",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "Investment",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "Groups",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "Groups",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "CustomEntityTypes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "CustomEntityTypes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "CustomEntities",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "CustomEntities",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "RecordedActivities");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "InvestmentInfluenceFactor");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "InvestmentInfluenceFactor");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "CustomEntityTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "CustomEntityTypes");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "CustomEntities");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "CustomEntities");

            migrationBuilder.RenameColumn(
                name: "LastModifiedTime",
                table: "RecordedActivities",
                newName: "AtTime");
        }
    }
}
