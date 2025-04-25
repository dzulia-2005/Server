using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Server.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Server.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBcontext _context;
        public StockRepository(ApplicationDBcontext context)
        {
            _context = context;
        }
        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stock.ToListAsync();
        }
    }
}