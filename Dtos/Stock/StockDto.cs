using System.Collections.Generic;
using Server.Dto.Comment;

namespace Server.Dto.Stock
{
    public class StockDto
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDividend { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public string? ImageUrl { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}