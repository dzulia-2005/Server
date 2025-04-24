using API.Models;
using Server.Dtos.Stock;

namespace Server.Mappers
{
    public static class StockMappers
    {
        public static StockDtos ToStockDto (this Stock stockModel) 
        {
            return new StockDtos 
            {
                ID = stockModel.ID,
                Title = stockModel.Title,
                Company = stockModel.Company,
                Purchase = stockModel.Purchase,
                LastDividend = stockModel.LastDividend,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }
    }
}