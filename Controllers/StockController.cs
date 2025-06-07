using System;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Mappers;
using System.Linq;
using Server.Dto.Stock;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces;
using Server.Helpers;

namespace Server.Controllers
{
    [Route("api/stock")]
    [ApiController]

    public class StockController: ControllerBase
    {
        private readonly ApplicationDBcontext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBcontext context,IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet("getourstock")]
        [Authorize]
        public async Task<IActionResult> getOurStocks()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userStock = await _stockRepo.GetStocksUserByIdAsync(userId);
            return Ok(userStock.Select(s=>s.ToStockDto()));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetAllAsync(query);
            var stocksDto = stock.Select(x => x.ToStockDto()).ToList();
            return Ok(stock);
            
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetByIdAsync(id);
            
            if(stock == null){  
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateStockRequestDto stockDto)
        {
            string? imageUrl = null;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (stockDto.ImageUrl != null && stockDto.ImageUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(stockDto.ImageUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await stockDto.ImageUrl.CopyToAsync(stream);
                }

                imageUrl = "/uploads/" + fileName;
            }

            var stock = new Stock
            {
                Title = stockDto.Title,
                Company = stockDto.Company,
                LastDividend = stockDto.LastDividend,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                ImageUrl = imageUrl,
                CreatedUserById = userId
            };

            _context.Stock.Add(stock);
            await _context.SaveChangesAsync();

            return Ok(stock);
        }

        

        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult>  Update([FromRoute] int id, [FromForm] UpdateStockRequestDto updateDto)
        {
            
            string? imageUrl = null;
            
            if (updateDto.ImageUrl != null && updateDto.ImageUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateDto.ImageUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateDto.ImageUrl.CopyToAsync(stream);
                }

                imageUrl = "/uploads/" + fileName;
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var StockModel = await _stockRepo.UpdateAsync(id, updateDto, imageUrl);
            
            if(StockModel == null)
            {
                return NotFound();
            }

            return Ok(StockModel.ToStockDto());
        }


        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StockModel = await _stockRepo.DeleteAsync(id);
            if(StockModel == null)
            {
                return NotFound();
            }
            
            
            return NoContent();
        }
        
    }
}