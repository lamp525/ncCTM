namespace CTM.Win.Models
{
    public class StockPoolInfoModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        public string StockCode { get; set; }

        public string StockFullCode { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        public string StockName { get; set; }

        ///// <summary>
        ///// 开始日期
        ///// </summary>
        //public DateTime FromDate { get; set; }

        ///// <summary>
        ///// 结束日期
        ///// </summary>
        //public DateTime? ToDate { get; set; }

        /// <summary>
        /// 目标负责人代码
        /// </summary>
        public string TargetPrincipal { get; set; }

        public string TargetPrincipalName { get; set; }

        /// <summary>
        /// 波段负责人代码
        /// </summary>
        public string BandPrincipal { get; set; }

        public string BandPrincipalName { get; set; }

        public string Remarks { get; set; }
    }
}