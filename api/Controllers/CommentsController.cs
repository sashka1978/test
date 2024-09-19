using api.Data;
using api.DTOs;
using api.DTOs.Comments;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentsController :ControllerBase
    {
         private readonly ICommentsRepository _commentRepo;
         private readonly IStockRepository _stockRepo;
         private readonly IFMPService _fmpService;
         private readonly UserManager<AppUser> _userManager;
         public CommentsController( ICommentsRepository commentRepo, IStockRepository stockRepo
         ,UserManager<AppUser> userManager, IFMPService fMPService)
       {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmpService = fMPService;
       }
       [HttpGet]
       [Authorize]
       public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
       {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var comments = await _commentRepo.GetAllAsync(queryObject);

            var commentDto = comments.Select(s=>s.toCommentsDto());

            return Ok(commentDto);
       }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.toCommentsDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute]int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment= await _commentRepo.UpdateAsync(id, updateCommentRequestDto.toCommentFromCUpdate());
            if(comment == null)
            {
                return NotFound("Commnet not found");
            }

            return Ok(comment.toCommentsDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute]int id)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var commentModel= await _commentRepo.DeleteAsync(id);
            if(commentModel == null)
            {
                return NotFound();
            }           
           
            return NoContent();
        }
        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> AddComment([FromRoute] string symbol, CreateCommentsRequestDto commentDto)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if(stock==null)
                    return BadRequest("stock doesn't exist");

                await _stockRepo.CreateAsync(stock);
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            
            var commentModel = commentDto.toCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id;

            await _commentRepo.CreateAsync(commentModel);
            
            return CreatedAtAction(nameof(GetById), new {
               id = commentModel.Id 
            }, commentModel.toCommentsDto());
        }
    }
}