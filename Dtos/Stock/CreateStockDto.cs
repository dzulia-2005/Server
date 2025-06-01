using System.ComponentModel.DataAnnotations;

namespace Server.Dto.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title must be min 5 characters")]
        [MaxLength(280,ErrorMessage = "title must be max 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(10,ErrorMessage = "company name cannot be over 10 characters")]
        public string Company { get; set; } = string.Empty;
        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.01,100)]
        public decimal LastDividend { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage = "industry cannot be over 10 characters")]
        public string Industry {get;set;}=string.Empty;
        [Required]
        [Range(1,500000000)]
        public long MarketCap { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}