using System.Threading.Tasks;
using Server.Interfaces;
using Server.Mappers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Dto.Comment;



namespace Server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo,IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var CommentDto = comments.Select(x=>x.ToCommentDto());
            return Ok(CommentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{StockId}")]
        public async Task<IActionResult> Create([FromRoute] int StockId, CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExists(StockId))
            {
                BadRequest("Stock does not exist");
            }

            var CommentModel = commentDto.ToCommentFromCreate(StockId);
            await _commentRepo.CreateAsync(CommentModel);
            return CreatedAtAction(nameof(GetById),new { id = CommentModel.ID },CommentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id,[FromBody] UpdateCommentRequestDto updateDto)
        {
            var comment = await _commentRepo.UpdateAsync(Id,updateDto.ToCommentFromUpdate(Id));
            if (comment == null)
            {
                NotFound("comment not found");
            }

            return Ok(comment.ToCommentDto());
        }
    }
}