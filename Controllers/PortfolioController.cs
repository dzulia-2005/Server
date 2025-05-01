using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Extension;
using Server.Interfaces;
using Server.Repository;

namespace Server.Controllers;
[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IStockRepository StockRepository;
    private readonly PortfolioRepository PortfolioRepository;

    public PortfolioController(UserManager<AppUser> userManager, IStockRepository StockRepository,PortfolioRepository portfolioRepository)
    {
        this.userManager = userManager;
        this.StockRepository = StockRepository;
        this.PortfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        var appUser = await this.userManager.FindByNameAsync(username);
        var userPortfolio = await PortfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }
}