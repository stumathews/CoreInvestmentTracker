using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoreInvestmentTracker.Migrations
{
    public partial class InitialDbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomEntityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DataType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEntityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Points = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Groups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Investment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DesirabilityStatement = table.Column<string>(nullable: true),
                    InitialInvestment = table.Column<float>(nullable: false),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    Value = table.Column<float>(nullable: false),
                    ValueProposition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentInfluenceFactor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Influence = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentInfluenceFactor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    OwningEntityId = table.Column<int>(nullable: false),
                    OwningEntityType = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => new { x.OwningEntityId, x.OwningEntityType, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false),
                    TimeZone = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    CustomEntityTypeId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwningCustomEntityId = table.Column<int>(nullable: true),
                    OwningEntityId = table.Column<int>(nullable: false),
                    OwningEntityType = table.Column<int>(nullable: false),
                    Points = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "InvestmentGroup_Investment",
                columns: table => new
                {
                    InvestmentID = table.Column<int>(nullable: false),
                    InvestmentGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InvestmentID_InvestmentGroupID", x => new { x.InvestmentID, x.InvestmentGroupID });
                    table.ForeignKey(
                        name: "FK_InvestmentGroup_Investment_Groups_InvestmentGroupID",
                        column: x => x.InvestmentGroupID,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentGroup_Investment_Investment_InvestmentID",
                        column: x => x.InvestmentID,
                        principalTable: "Investment",
                        principalColumn: "Id",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentInfluenceFactor_Investment_InvestmentInfluenceFactor_InvestmentInfluenceFactorID",
                        column: x => x.InvestmentInfluenceFactorID,
                        principalTable: "InvestmentInfluenceFactor",
                        principalColumn: "Id",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_Investment_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "Id",
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
                    table.PrimaryKey("PrimaryKey_InvestmentID_InvestmentRiskID", x => new { x.InvestmentID, x.InvestmentRiskID });
                    table.ForeignKey(
                        name: "FK_InvestmentRisk_Investment_Investment_InvestmentID",
                        column: x => x.InvestmentID,
                        principalTable: "Investment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentRisk_Investment_Risks_InvestmentRiskID",
                        column: x => x.InvestmentRiskID,
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordedActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OwningEntityId = table.Column<int>(nullable: false),
                    OwningEntityType = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    IsFlagged = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<long>(nullable: false),
                    Tag = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_Id_OwningEntityId_OwningEntityType", x => new { x.Id, x.OwningEntityId, x.OwningEntityType });
                    table.ForeignKey(
                        name: "FK_RecordedActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_CustomEntities_CustomEntityTypeId",
                table: "CustomEntities",
                column: "CustomEntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEntities_OwningCustomEntityId",
                table: "CustomEntities",
                column: "OwningCustomEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEntity_Investment_CustomEntityId",
                table: "CustomEntity_Investment",
                column: "CustomEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ParentId",
                table: "Groups",
                column: "ParentId");

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
                name: "IX_RecordedActivities_UserId",
                table: "RecordedActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_Investment_RegionID",
                table: "Region_Investment",
                column: "RegionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomEntity_Investment");

            migrationBuilder.DropTable(
                name: "InvestmentGroup_Investment");

            migrationBuilder.DropTable(
                name: "InvestmentInfluenceFactor_Investment");

            migrationBuilder.DropTable(
                name: "InvestmentRisk_Investment");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "RecordedActivities");

            migrationBuilder.DropTable(
                name: "Region_Investment");

            migrationBuilder.DropTable(
                name: "CustomEntities");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "InvestmentInfluenceFactor");

            migrationBuilder.DropTable(
                name: "Risks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Investment");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "CustomEntityTypes");
        }
    }
}
