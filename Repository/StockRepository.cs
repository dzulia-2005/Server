using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using Server.Dto.Stock;

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
            return _context.Stock.Include(x => x.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(x => x.Comments).FirstOrDefaultAsync(x=>x.ID==id);
        } 

        public async Task<Stock?> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var exsitingStock = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);

            if(exsitingStock == null)
            {
                return null;
            }

            exsitingStock.Title = stockDto.Title;
            exsitingStock.Company = stockDto.Company;
            exsitingStock.Purchase = stockDto.Purchase;
            exsitingStock.LastDividend = stockDto.LastDividend;
            exsitingStock.Industry = stockDto.Industry;
            exsitingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return exsitingStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var StockModel = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if(StockModel == null)
            {
                return null;
            }
            _context.Stock.Remove(StockModel);
            await _context.SaveChangesAsync();

            return StockModel;
            
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(s=>s.ID == id);
        }

    }
}