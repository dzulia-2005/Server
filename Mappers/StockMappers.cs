using System.Runtime.Intrinsics.X86;
using API.Models;
using Server.Dto.Stock;
using API.Models;
using Server.Dto.Comment;

namespace Server.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto (this Stock stockModel) 
        {
            return new StockDto
            {
                ID = stockModel.ID,
                Title = stockModel.Title,
                Company = stockModel.Company,
                Purchase = stockModel.Purchase,
                LastDividend = stockModel.LastDividend,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments != null 
                    ? stockModel.Comments.Select(x => x.ToCommentDto()).ToList()
                    : new List<CommentDto>(),            };
        }

        public static Stock toStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Company = stockDto.Company,
                Purchase = stockDto.Purchase,
                LastDividend = stockDto.LastDividend,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                Title = stockDto.Title
            };
        }
    }
}