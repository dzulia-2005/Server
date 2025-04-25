using System;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Mappers;
using System.Linq;
using Server.Dto.Stock;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces;

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

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var stock = await _stockRepo.GetAllAsync();
            var stocksDto = stock.Select(x => x.ToStockDto());
            return Ok(stock);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var stock = await _context.Stock.FindAsync(id);
            
            if(stock == null){  
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var StockModel = stockDto.toStockFromCreateDTO();
            await _context.Stock.AddAsync(StockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {ID=StockModel.ID},StockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>  Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var StockModel = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if(StockModel == null)
            {
                return NotFound();
            }

            StockModel.Title = updateDto.Title;
            StockModel.Company = updateDto.Company;
            StockModel.Purchase = updateDto.Purchase;
            StockModel.LastDividend = updateDto.LastDividend;
            StockModel.Industry = updateDto.Industry;
            StockModel.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();
            return Ok(StockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var StockModel = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if(StockModel == null)
            {
                return NotFound();
            }
            _context.Stock.Remove(StockModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}