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
        public async Task<IActionResult> GetAll()
        {
            var stock = await _stockRepo.GetAllAsync();
            var stocksDto = stock.Select(x => x.ToStockDto());
            return Ok(stock);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            
            if(stock == null){  
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var StockModel = stockDto.toStockFromCreateDTO();
            await _stockRepo.CreateAsync(StockModel);
            return CreatedAtAction(nameof(GetById),new {ID=StockModel.ID},StockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>  Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var StockModel = await _stockRepo.UpdateAsync(id,updateDto);
            if(StockModel == null)
            {
                return NotFound();
            }

            return Ok(StockModel.ToStockDto());
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var StockModel = await _stockRepo.DeleteAsync(id);
            if(StockModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}