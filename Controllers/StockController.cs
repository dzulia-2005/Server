using System;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Mappers;
using System.Linq;
using Server.Dto.Stock;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
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

        [HttpGet]
        [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StockModel = stockDto.toStockFromCreateDTO();
            await _stockRepo.CreateAsync(StockModel);
            return CreatedAtAction(nameof(GetById),new {ID=StockModel.ID},StockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult>  Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StockModel = await _stockRepo.UpdateAsync(id,updateDto);
            if(StockModel == null)
            {
                return NotFound();
            }

            return Ok(StockModel.ToStockDto());
        }


        [HttpDelete("{id:int}")]
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