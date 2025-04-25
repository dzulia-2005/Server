namespace Server.Dto.Stock
{
    public class DeleteStockRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDividend { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}