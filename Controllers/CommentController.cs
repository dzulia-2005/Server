using System.Threading.Tasks;
using Server.Interfaces;
using Server.Mappers;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Dto.Comment;
using Server.Extension;


namespace Server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepo,IStockRepository stockRepo,UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync();
            var CommentDto = comments.Select(x=>x.ToCommentDto());
            return Ok(CommentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{StockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int StockId, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepo.StockExists(StockId))
            {
                BadRequest("Stock does not exist");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            
            var CommentModel = commentDto.ToCommentFromCreate(StockId);
            CommentModel.AppUserId = appUser.Id;
            
            await _commentRepo.CreateAsync(CommentModel);
            return CreatedAtAction(nameof(GetById),new { id = CommentModel.ID },CommentModel.ToCommentDto());
        }

        [HttpPut("edit/{Id:int}")]
        public async Task<IActionResult> Update([FromRoute] int Id,[FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.UpdateAsync(Id,updateDto.ToCommentFromUpdate(Id));
            if (comment == null)
            {
                NotFound("comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("comment does not exist");
            }

            return NoContent();
        }
    }
}