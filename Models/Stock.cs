using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
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
    }
}