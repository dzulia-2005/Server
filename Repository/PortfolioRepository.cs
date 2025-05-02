using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces;

namespace Server.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDBcontext _context;
    public PortfolioRepository(ApplicationDBcontext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
                {
                    ID = stock.StockId,
                    Purchase = stock.stock.Purchase,
                    Company = stock.stock.Company,
                    LastDividend = stock.stock.LastDividend,
                    Industry = stock.stock.Industry,
                    MarketCap = stock.stock.MarketCap,
                    Title = stock.stock.Title,
                }
            ).ToListAsync();
    }

    public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }

    public async Task<Portfolio> DeletePortfolio(AppUser user, string company)
    {
        var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x=>x.AppUserId==user.Id&& x.stock.Company.ToLower() == company.ToLower());
        if (portfolioModel == null)
        {
            return null;
        }

        _context.Portfolios.Remove(portfolioModel);
        await _context.SaveChangesAsync();
        return portfolioModel;
    }

}