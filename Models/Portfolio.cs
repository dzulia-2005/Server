using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("portfolios")]
public class Portfolio
{
    public string AppUserId { get; set; }
    public int StockId { get; set; }
    
    public AppUser AppUser { get; set; }
    public Stock stock { get; set; }
}