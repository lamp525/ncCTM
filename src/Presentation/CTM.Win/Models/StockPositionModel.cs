namespace CTM.Win.Models
{
    public class StockPositionModel
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string SecurityCompanyName { get; set; }

        public string AttributeName { get; set; }

        public int DepartmentId { get; set; }

        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        public string StockName { get; set; }

        public string StockFullCode { get; set; }

        public int StockHoldingVolume { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal PositionValue { get; set; }
    }
}