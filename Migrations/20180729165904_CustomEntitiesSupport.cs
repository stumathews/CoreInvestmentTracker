using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreInvestmentTracker.Migrations
{
    public partial class CustomEntitiesSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomEntityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEntityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomEntityTypeId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OwningCustomEntityId = table.Column<int>(nullable: true),
                    OwningEntityId = table.Column<int>(nullable: false),
                    OwningEntityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_Id_CustomEntityTypeId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomEntities_CustomEntityTypes_CustomEntityTypeId",
                        column: x => x.CustomEntityTypeId,
                        principalTable: "CustomEntityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomEntities_CustomEntities_OwningCustomEntityId",
                        column: x => x.OwningCustomEntityId,
                        principalTable: "CustomEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomEntities_CustomEntityTypeId",
                table: "CustomEntities",
                column: "CustomEntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEntities_OwningCustomEntityId",
                table: "CustomEntities",
                column: "OwningCustomEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomEntities");

            migrationBuilder.DropTable(
                name: "CustomEntityTypes");
        }
    }
}
