namespace CoreInvestmentTracker.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class InvestmentInfluenceFactor_Investment
    {
        /// <summary>
        /// Investment ID
        /// </summary>
        public int InvestmentID { get; set; }
        /// <summary>
        /// Investment
        /// </summary>
        public Investment Investment { get; set; }
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
    public class Region_Investment
    {
        /// <summary>
        /// 
        /// </summary>
        public int InvestmentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Investment Investment { get; set; }
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
    public class InvestmentRisk_Investment
    {
        /// <summary>
        /// 
        /// </summary>
        public int InvestmentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Investment Investment { get; set; }

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
    public class InvestmentGroup_Investment
    {
        /// <summary>
        /// 
        /// </summary>
        public int InvestmentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Investment Investment { get; set; }

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

}