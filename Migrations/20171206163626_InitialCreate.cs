using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreInvestmentTracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Investment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DesirabilityStatement = table.Column<string>(nullable: true),
                    InitialInvestment = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    Value = table.Column<float>(nullable: false),
                    ValueProposition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentInfluenceFactor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Influence = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentInfluenceFactor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentGroup_Investment",
                columns: table => new
                {
                    InvestmentID = table.Column<int>(nullable: false),
                    InvestmentGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InvestmentID_InvestmentGroup", x => new { x.InvestmentID, x.InvestmentGroupID });
                    table.ForeignKey(
                        name: "FK_InvestmentGroup_Investment_Groups_InvestmentGroupID",
                        column: x => x.InvestmentGroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentGroup_Investment_Investment_InvestmentID",
                        column: x => x.InvestmentID,
                        principalTable: "Investment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentInfluenceFactor_Investment",
                columns: table => new
                {
                    InvestmentID = table.Column<int>(nullable: false),
                    InvestmentInfluenceFactorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InvestmentID_InvestmentInfluenceFactorID", x => new { x.InvestmentID, x.InvestmentInfluenceFactorID });
                    table.ForeignKey(
                        name: "FK_InvestmentInfluenceFactor_Investment_Investment_InvestmentID",
                        column: x => x.InvestmentID,
                        principalTable: "Investment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFactor_InvestmentInfluenceFactorID",
                        column: x => x.InvestmentInfluenceFactorID,
                        principalTable: "InvestmentInfluenceFactor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region_Investment",
                columns: table => new
                {
                    InvestmentID = table.Column<int>(nullable: false),
                    RegionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InvestmentID_RegionID", x => new { x.InvestmentID, x.RegionID });
                    table.ForeignKey(
                        name: "FK_Region_Investment_Investment_InvestmentID",
                        column: x => x.InvestmentID,
                        principalTable: "Investment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_Investment_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentRisk_Investment",
                columns: table => new
                {
                    InvestmentID = table.Column<int>(nullable: false),
                    InvestmentRiskID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InvestmentID_InvestmentRisk", x => new { x.InvestmentID, x.InvestmentRiskID });
                    table.ForeignKey(
                        name: "FK_InvestmentRisk_Investment_Investment_InvestmentID",
                        column: x => x.InvestmentID,
                        principalTable: "Investment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentRisk_Investment_Risks_InvestmentRiskID",
                        column: x => x.InvestmentRiskID,
                        principalTable: "Risks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentGroup_Investment_InvestmentGroupID",
                table: "InvestmentGroup_Investment",
                column: "InvestmentGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFactorID",
                table: "InvestmentInfluenceFactor_Investment",
                column: "InvestmentInfluenceFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentRisk_Investment_InvestmentRiskID",
                table: "InvestmentRisk_Investment",
                column: "InvestmentRiskID");

            migrationBuilder.CreateIndex(
                name: "IX_Region_Investment_RegionID",
                table: "Region_Investment",
                column: "RegionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentGroup_Investment");

            migrationBuilder.DropTable(
                name: "InvestmentInfluenceFactor_Investment");

            migrationBuilder.DropTable(
                name: "InvestmentRisk_Investment");

            migrationBuilder.DropTable(
                name: "Region_Investment");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "InvestmentInfluenceFactor");

            migrationBuilder.DropTable(
                name: "Risks");

            migrationBuilder.DropTable(
                name: "Investment");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
