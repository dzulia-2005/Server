using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Comments")]
    public class Comments
    {
        public int ID { get; set; }
        public string Title {get;set;}=string.Empty;
        public string Content {get;set;} = string.Empty;
        public DateTime CreateOn {get;set;} = DateTime.Now;
        public int? StockID {get;set;}
        public Stock? stock {get;set;}
    }
}