namespace CTM.Core.Domain.Stock
{
    public class StockPoolInfo : BaseEntity
    {
        /// <summary>
        /// 股票ID
        /// </summary>
        public int StockId { get; set; }

        /// <summary>
        /// 目标负责人代码
        /// </summary>
        public string TargetPrincipal { get; set; }

        /// <summary>
        /// 波段负责人代码
        /// </summary>
        public string BandPrincipal { get; set; }

        /// <summary>
        /// 日内负责人代码
        /// </summary>
        public string DayPrincipal { get; set; }

        public string Remarks { get; set; }

        public virtual StockInfo StockInfo { get; set; }

        public virtual string TargetName { get; set; }

        public virtual string BandName { get; set; }
    }
}