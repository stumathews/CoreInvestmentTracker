namespace CoreInvestmentTracker.Models
{
    public class InvestmentInfluenceFactor_Investment
    {
        public int InvestmentID { get; set; }
        public Investment Investment { get; set; }

        public int InvestmentInfluenceFactorID { get; set; }
        public InvestmentInfluenceFactor InvestmentInfluenceFactor { get; set; }
    }

    public class Region_Investment
    {
        public int InvestmentID { get; set; }
        public Investment Investment { get; set; }

        public int RegionID { get; set; }
        public Region Region { get; set; }
    }

    public class InvestmentRisk_Investment
    {
        public int InvestmentID { get; set; }
        public Investment Investment { get; set; }

        public int InvestmentRiskID { get; set; }
        public InvestmentRisk InvestmentRisk { get; set; }
    }
    public class InvestmentGroup_Investment
    {
        public int InvestmentID { get; set; }
        public Investment Investment { get; set; }

        public int InvestmentGroupID { get; set; }
        public InvestmentGroup InvestmentGroup { get; set; }
    }

}