using CoreInvestmentTracker.Models.DEL;
using Microsoft.EntityFrameworkCore;

namespace CoreInvestmentTracker.Models.DAL
{
    /// <summary>
    /// This will manage our interaction with the database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)        
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
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Investment>().ToTable("Investment");
            modelBuilder.Entity<InvestmentInfluenceFactor>().ToTable("InvestmentInfluenceFactor");

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

            modelBuilder.Entity<InvestmentNote>().HasKey(i => new { i.OwningEntityId, i.OwningEntityType, i.ID });

            /*
            modelBuilder.Entity<InvestmentGroup_ChildInvestmentGroup>()
            .HasKey(i => new { i.InvestmentGroupID, i. })
            .HasName("PrimaryKey_InvestmentGroupID_ChildInvestmentGroupID"); */
            
        }
    }
}