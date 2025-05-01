using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        [Column(TypeName="decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal LastDividend { get; set; }
        public string Industry {get;set;}=string.Empty;
        public long MarketCap { get; set; }
        public List<Comments> Comments { get; set; }
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}