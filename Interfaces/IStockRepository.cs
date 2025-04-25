using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace Server.Interfaces
{
    public interface IStockRepository
    {
         Task<List<Stock>> GetAllAsync(); 
    }
}