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

    public PortfolioController(UserManager<AppUser> userManager, IStockRepository StockRepository,IPortfolioRepository portfolioRepository)
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
        Console.WriteLine($"Username:{username}");
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _PortfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]

    public async Task<IActionResult> AddPortfolio(string company)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = await _StockRepository.GetCompanyAsync(company);
        if (stock == null)
        {
            return BadRequest("stock is not found");
        }

        var userPortfolio = await _PortfolioRepository.GetUserPortfolio(appUser);
        if (userPortfolio.Any(e=>e.Company.ToLower()==company.ToLower()))
        {
            return BadRequest("can not add same stock to portfolio");
        }

        var portfolioModel = new Portfolio
        {
            StockId = stock.ID,
            AppUserId = appUser.Id,
            
        };

        await _PortfolioRepository.CreatePortfolio(portfolioModel);
        if (portfolioModel==null)
        {
            return StatusCode(500, "Could not create");
        }
        else
        {
            return Created();
        }
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string company)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _PortfolioRepository.GetUserPortfolio(appUser);
        var filteredStock = userPortfolio.Where(u=>u.Company.ToLower() == company.ToLower()).ToList();
        if (filteredStock.Count == 1)
        {
            await _PortfolioRepository.DeletePortfolio(appUser,company);
        }
        else
        {
            return BadRequest("stock not in your portfolio");
        }

        return Ok();

    }
}