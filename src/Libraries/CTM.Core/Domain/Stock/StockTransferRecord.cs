namespace CTM.Core.Domain.Stock
{
    public class StockTransferRecord : BaseEntity
    {
        public int TransferId { get; set; }

        public int RecordId { get; set; }
    }
}