using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Server.Dto.Stock;

namespace Server.Interfaces
{
    public interface IStockRepository
    {
         Task<List<Stock>> GetAllAsync(); 
         Task<Stock?> GetByIdAsync(int id);
         Task<Stock?> CreateAsync(Stock stockModel);
         Task<Stock?> UpdateAsync(int id,UpdateStockRequestDto stockDto);
         Task<Stock?> DeleteAsync(int id);
    }
}