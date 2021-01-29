using CoreInvestmentTracker.Models.DEL;
using Microsoft.EntityFrameworkCore;

namespace CoreInvestmentTracker.Models.DAL
{
    /// <inheritdoc />
    /// <summary>
    /// This will manage our interaction with the database
    /// </summary>
    public class InvestmentDbContext : DbContext
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public InvestmentDbContext(DbContextOptions options) : base(options) { /* Require parameterless constructor */ }

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
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Audit log
        /// </summary>
        public DbSet<RecordedActivity> RecordedActivities { get; set; }

        /// <summary>
        /// User defined entities
        /// </summary>
        public DbSet<CustomEntity> CustomEntities { get; set; }

        /// <summary>
        /// Types of custom entities that exist in the system
        /// </summary>
        public DbSet<CustomEntityType> CustomEntityTypes { get; set; }

        /// <summary>
        /// We have a list of transactions
        /// </summary>
        public DbSet<InvestmentTransaction> Transactions { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investment>().ToTable("Investment");
            modelBuilder.Entity<Investment>().Ignore(x => x.InvestmentIds);
            modelBuilder.Entity<InvestmentInfluenceFactor>().ToTable("InvestmentInfluenceFactor");
            modelBuilder.Entity<InvestmentInfluenceFactor>().Ignore(x => x.InvestmentIds);
            modelBuilder.Entity<InvestmentGroup>().Ignore(x => x.InvestmentIds);
            modelBuilder.Entity<Region>().Ignore(x => x.InvestmentIds);
            modelBuilder.Entity<InvestmentNote>().Ignore(x => x.InvestmentIds);
            modelBuilder.Entity<InvestmentRisk>().Ignore(x => x.InvestmentIds);

            modelBuilder.Entity<InvestmentInfluenceFactor_Investment>()
                .HasKey(i => new {i.InvestmentID, i.InvestmentInfluenceFactorID})
                .HasName("PrimaryKey_InvestmentID_InvestmentInfluenceFactorID");

            modelBuilder.Entity<Region_Investment>()
                .HasKey(i => new {i.InvestmentID, i.RegionID})
                .HasName("PrimaryKey_InvestmentID_RegionID");

            modelBuilder.Entity<InvestmentGroup_Investment>()
                .HasKey(i => new {i.InvestmentID, i.InvestmentGroupID})
                .HasName("PrimaryKey_InvestmentID_InvestmentGroupID");

            modelBuilder.Entity<InvestmentRisk_Investment>()
                .HasKey(i => new {i.InvestmentID, i.InvestmentRiskID})
                .HasName("PrimaryKey_InvestmentID_InvestmentRiskID");

            modelBuilder.Entity<InvestmentNote>()
                .HasKey(i => new {i.OwningEntityId, i.OwningEntityType, ID = i.Id});

            modelBuilder.Entity<RecordedActivity>().HasKey(i => new {i.Id, i.OwningEntityId, i.OwningEntityType})
                .HasName("PrimaryKey_Id_OwningEntityId_OwningEntityType");

            modelBuilder.Entity<CustomEntity>().HasKey(i => new {i.Id})
                .HasName("PrimaryKey_Id_CustomEntityTypeId");

            modelBuilder.Entity<CustomEntity_Investment>()
                .HasKey(i => new {i.InvestmentID, i.CustomEntityId})
                .HasName("PrimaryKey_InvestmentID_CustomEntityID");
            
            modelBuilder.Entity<InvestmentTransaction>()
                .HasKey(k => new {k.Id})
                .HasName("PrimaryKey_Id");

            modelBuilder.Entity<InvestmentTransaction>().HasAlternateKey(a => a.Name); // Cant have two transactions identified by the same name - name acts as a client identifier

        }
    }
}