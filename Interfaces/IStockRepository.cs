using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Server.Dto.Stock;
using Server.Helpers;

namespace Server.Interfaces
{
    public interface IStockRepository
    {
         Task<List<Stock>> GetAllAsync(QueryObject query); 
         Task<Stock?> GetByIdAsync(int id);
         Task<Stock?> GetCompanyAsync(string symbol);
         Task<Stock?> CreateAsync(Stock stockModel);
         Task<Stock?> UpdateAsync(int id,UpdateStockRequestDto stockDto);
         Task<Stock?> DeleteAsync(int id);
         Task<bool> StockExists(int id);
    }
}