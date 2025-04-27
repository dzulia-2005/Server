using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace Server.Interfaces
{
    public interface ICommentRepository
    {
         Task<List<Comments>> GetAllAsync(); 
         Task<Comments?> GetByIdAsync(int id);
         Task<Comments?> CreateAsync(Comments commentModel);
         Task<Comments?> UpdateAsync(int id,Comments commentModel);
    }
}