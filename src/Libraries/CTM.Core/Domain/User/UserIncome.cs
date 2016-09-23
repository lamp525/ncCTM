using System;

namespace CTM.Core.Domain.User
{
    public class UserIncome : BaseEntity
    {
        public string UserCode { get; set; }

        public DateTime TradeDate { get; set; }

        public int WeekDay { get; set; }

        public int WeekOfYear { get; set; }

        public decimal CurrentAsset { get; set; }

        public decimal IncomeAmount { get; set; }

        public decimal IncomeRatio { get; set; }

        public decimal TotalIncomeAmount { get; set; }

        public decimal TotalIncomeRatio { get; set; }

        public decimal OccupyAmount { get; set; }

        public decimal InputAmount { get; set; }

        public decimal AvailableAmount { get; set; }

        public decimal PositionRatio { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateUser { get; set; }

        public string Remarks { get; set; }
    }
}