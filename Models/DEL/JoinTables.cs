using CoreInvestmentTracker.Models.DEL;

namespace CoreInvestmentTracker.Models
{
    public interface IWithAnInvestment
    {
        /// <summary>
        /// Investment ID
        /// </summary>
        int InvestmentID { get; set; }
        /// <summary>
        /// Investment
        /// </summary>
        Investment Investment { get; set; }
    }

    public abstract class WithAnInvestment : IWithAnInvestment
    {
        /// <summary>
        /// 
        /// </summary>
        public int InvestmentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Investment Investment { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class InvestmentInfluenceFactor_Investment : WithAnInvestment
    {
        
        /// <summary>
        /// InvestmentInfluenceFactorID
        /// </summary>
        public int InvestmentInfluenceFactorID { get; set; }
        /// <summary>
        /// InvestmentInfluenceFactor
        /// </summary>
        public InvestmentInfluenceFactor InvestmentInfluenceFactor { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Region_Investment : WithAnInvestment
    {
        
        /// <summary>
        /// 
        /// </summary>
        public int RegionID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Region Region { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InvestmentRisk_Investment : WithAnInvestment
    {
        /// <summary>
        /// 
        /// </summary>
        public int InvestmentRiskID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvestmentRisk InvestmentRisk { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class InvestmentGroup_Investment : WithAnInvestment
    {
        /// <summary>
        /// 
        /// </summary>
        public int InvestmentGroupID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvestmentGroup InvestmentGroup { get; set; }
    }

    public class InvestmentGroup_ChildInvestmentGroup
    {
        /// <summary>
        /// 
        /// </summary>
        public int InvestmentGroupID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvestmentGroup InvestmentGroup { get; set; }

        /*
        public int ChildInvestmentGroupID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvestmentGroup ChildInvestmentGroup { get; set; } */
    }

    /// <summary>
    /// Custom Entities can link to investments and vice reversa
    /// </summary>
    public class CustomEntity_Investment : WithAnInvestment
    {

        /// <summary>
        /// CustomEntityId
        /// </summary>
        public int CustomEntityId { get; set; }

        /// <summary>
        /// CustomEntity
        /// </summary>
        public CustomEntity CustomEntity { get; set; }


    }

}