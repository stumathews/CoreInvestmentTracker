﻿// <auto-generated />
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CoreInvestmentTracker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreInvestmentTracker.Models.Investment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DesirabilityStatement");

                    b.Property<float>("InitialInvestment");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Symbol");

                    b.Property<float>("Value");

                    b.Property<string>("ValueProposition");

                    b.HasKey("ID");

                    b.ToTable("Investment");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentGroup_Investment", b =>
                {
                    b.Property<int>("InvestmentID");

                    b.Property<int>("InvestmentGroupID");

                    b.HasKey("InvestmentID", "InvestmentGroupID")
                        .HasName("PrimaryKey_InvestmentID_InvestmentGroup");

                    b.HasIndex("InvestmentGroupID");

                    b.ToTable("InvestmentGroup_Investment");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentInfluenceFactor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Influence");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("InvestmentInfluenceFactor");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentInfluenceFactor_Investment", b =>
                {
                    b.Property<int>("InvestmentID");

                    b.Property<int>("InvestmentInfluenceFactorID");

                    b.HasKey("InvestmentID", "InvestmentInfluenceFactorID")
                        .HasName("PrimaryKey_InvestmentID_InvestmentInfluenceFactorID");

                    b.HasIndex("InvestmentInfluenceFactorID");

                    b.ToTable("InvestmentInfluenceFactor_Investment");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentRisk", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.ToTable("Risks");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentRisk_Investment", b =>
                {
                    b.Property<int>("InvestmentID");

                    b.Property<int>("InvestmentRiskID");

                    b.HasKey("InvestmentID", "InvestmentRiskID")
                        .HasName("PrimaryKey_InvestmentID_InvestmentRisk");

                    b.HasIndex("InvestmentRiskID");

                    b.ToTable("InvestmentRisk_Investment");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.Region", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.Region_Investment", b =>
                {
                    b.Property<int>("InvestmentID");

                    b.Property<int>("RegionID");

                    b.HasKey("InvestmentID", "RegionID")
                        .HasName("PrimaryKey_InvestmentID_RegionID");

                    b.HasIndex("RegionID");

                    b.ToTable("Region_Investment");
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentGroup_Investment", b =>
                {
                    b.HasOne("CoreInvestmentTracker.Models.InvestmentGroup", "InvestmentGroup")
                        .WithMany("Investments")
                        .HasForeignKey("InvestmentGroupID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreInvestmentTracker.Models.Investment", "Investment")
                        .WithMany("Groups")
                        .HasForeignKey("InvestmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentInfluenceFactor_Investment", b =>
                {
                    b.HasOne("CoreInvestmentTracker.Models.Investment", "Investment")
                        .WithMany("Factors")
                        .HasForeignKey("InvestmentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreInvestmentTracker.Models.InvestmentInfluenceFactor", "InvestmentInfluenceFactor")
                        .WithMany("Investments")
                        .HasForeignKey("InvestmentInfluenceFactorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.InvestmentRisk_Investment", b =>
                {
                    b.HasOne("CoreInvestmentTracker.Models.Investment", "Investment")
                        .WithMany("Risks")
                        .HasForeignKey("InvestmentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreInvestmentTracker.Models.InvestmentRisk", "InvestmentRisk")
                        .WithMany("Investments")
                        .HasForeignKey("InvestmentRiskID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreInvestmentTracker.Models.Region_Investment", b =>
                {
                    b.HasOne("CoreInvestmentTracker.Models.Investment", "Investment")
                        .WithMany("Regions")
                        .HasForeignKey("InvestmentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreInvestmentTracker.Models.Region", "Region")
                        .WithMany("Investments")
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
