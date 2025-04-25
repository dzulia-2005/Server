using System;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Mappers;
using System.Linq;
using Server.Dto.Stock;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using API.Models;

namespace Server.Controllers
{
    [Route("api/stock")]
    [ApiController]

    public class StockController: ControllerBase
    {
        private readonly ApplicationDBcontext _context;
        public StockController(ApplicationDBcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var stock = _context.Stock.ToList().Select(i => i.ToStockDto());
            return Ok(stock);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id){
            var stock = _context.Stock.Find(id);
            
            if(stock == null){  
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var StockModel = stockDto.toStockFromCreateDTO();
            _context.Stock.Add(StockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),new {ID=StockModel.ID},StockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var StockModel = _context.Stock.FirstOrDefault(x => x.ID == id);
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

            _context.SaveChanges();
            return Ok(StockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public IActionResult Delete([FromRoute] int id)
        {
            var StockModel = _context.Stock.FirstOrDefault(x => x.ID == id);
            if(StockModel == null)
            {
                return NotFound();
            }
            _context.Stock.Remove(StockModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}