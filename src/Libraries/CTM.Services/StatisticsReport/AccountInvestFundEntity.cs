namespace CTM.Services.StatisticsReport
{
    public class AccountInvestFundEntity
    {
        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public string SecurityCompanyName { get; set; }

        public string AttributeName { get; set; }

        public decimal InitialAmount { get; set; }

        public decimal InAmount { get; set; }

        public decimal OutAmount { get; set; }

        public decimal FinalAMount { get; set; }
    }
}