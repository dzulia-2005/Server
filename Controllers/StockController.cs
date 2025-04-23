using API.Data;

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
        }
    }
}