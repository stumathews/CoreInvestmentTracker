﻿using CoreInvestmentTracker.Models.DEL;
using Microsoft.EntityFrameworkCore;

namespace CoreInvestmentTracker.Models.DAL
{
    /// <inheritdoc />
    /// <summary>
    /// This will manage our interaction with the database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        
        /// <summary>
        /// Investments
        /// </summary>
        public DbSet<Investment> Investments { get; set; }
        /// <summary>
        /// Groups
        /// </summary>
        public DbSet<InvestmentGroup> Groups { get; set; }
        /// <summary>
        /// Factors
        /// </summary>
        public DbSet<InvestmentInfluenceFactor> Factors { get; set; }
        /// <summary>
        /// Risks
        /// </summary>
        public DbSet<InvestmentRisk> Risks { get; set; }
        /// <summary>
        /// Regions
        /// </summary>
        public DbSet<Region> Regions { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public DbSet<InvestmentNote> Notes { get; set; }


        /// <summary>
        /// User information
        /// </summary>
        public DbSet<DEL.User> Users { get; set; }

        /// <summary>
        /// Audit log
        /// </summary>
        public DbSet<RecordedActivity> RecordedActivities { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            modelBuilder.Entity<Investment>().ToTable("Investment");
            modelBuilder.Entity<Investment>().Ignore(x => x.investmentIds);
            modelBuilder.Entity<InvestmentInfluenceFactor>().ToTable("InvestmentInfluenceFactor");
            modelBuilder.Entity<InvestmentInfluenceFactor>().Ignore(x => x.investmentIds);
            modelBuilder.Entity<InvestmentGroup>().Ignore(x => x.investmentIds);
            modelBuilder.Entity<Region>().Ignore(x => x.investmentIds);
            modelBuilder.Entity<InvestmentNote>().Ignore(x => x.investmentIds);
            modelBuilder.Entity<InvestmentRisk>().Ignore(x => x.investmentIds);

            modelBuilder.Entity<InvestmentInfluenceFactor_Investment>()            
                .HasKey(i => new { i.InvestmentID, i.InvestmentInfluenceFactorID })
                .HasName("PrimaryKey_InvestmentID_InvestmentInfluenceFactorID");

            modelBuilder.Entity<Region_Investment>()
                .HasKey(i => new { i.InvestmentID, i.RegionID })
                .HasName("PrimaryKey_InvestmentID_RegionID");

            modelBuilder.Entity<InvestmentGroup_Investment>()
                .HasKey(i => new { i.InvestmentID, i.InvestmentGroupID })
                .HasName("PrimaryKey_InvestmentID_InvestmentGroupID");

            modelBuilder.Entity<InvestmentRisk_Investment>()
               .HasKey(i => new { i.InvestmentID, i.InvestmentRiskID })
               .HasName("PrimaryKey_InvestmentID_InvestmentRiskID");

            modelBuilder.Entity<InvestmentNote>()
                .HasKey(i => new { i.OwningEntityId, i.OwningEntityType, ID = i.Id });
        }
    }
}