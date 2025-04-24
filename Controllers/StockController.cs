using System;
using API.Data;
using Microsoft.AspNetCore.Mvc;


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
            var stock = _context.Stock.ToList();
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id){
            var stock = _context.Stock.Find(id);
            
            if(stock == null){
                return NotFound();
            }
            
            return Ok(stock);
        }
    }
}