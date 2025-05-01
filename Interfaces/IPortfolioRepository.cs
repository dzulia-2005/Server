using API.Models;

namespace Server.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);
    Task<Portfolio> CreatePortfolio(Portfolio portfolio);
}