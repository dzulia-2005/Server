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
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _StockRepository;
    private readonly IPortfolioRepository _PortfolioRepository;

    public PortfolioController(UserManager<AppUser> userManager, IStockRepository StockRepository,
        IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _StockRepository = StockRepository;
        _PortfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        if (string.IsNullOrEmpty(username))
            return Unauthorized();
        var appUser = await _userManager.FindByNameAsync(username);
        
        if (appUser == null)
            return NotFound();
        
        var userPortfolio = await _PortfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }
}