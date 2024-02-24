using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Dtos.CommentDtos;
using api.Interfaces;
using api.Mappers;
using api.Services;

using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {



        private readonly IStockService stockService;
        private readonly ICommentService commentService;
        public CommentController(ICommentService commentService, IStockService stockService)
        {
            this.stockService = stockService;
            this.commentService = commentService;
        }

        [HttpGet("{stockId:int}")]
        public async Task<IActionResult> FindAll([FromRoute] int stockId)
        {
            return Ok(await commentService.FindAll(stockId));
        }

        [HttpGet("{stockId:int}/{commentId:int}")]
        public async Task<IActionResult> FindById([FromRoute] int stockId, [FromRoute] int commentId)
        {
            var commentResponseDto = await commentService.FindById(stockId, commentId);
            if (commentResponseDto == null)
            {
                return NotFound();
            }
            return Ok(commentResponseDto);

        }


        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Add([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            var commentResponseDto = await commentService.Add(stockId, createCommentDto);
            if (commentResponseDto == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(FindById), new { stockId, commentId = commentResponseDto.Id }, commentResponseDto);
        }


        [HttpPut("{stockId:int}/{commentId:int}")]
        public async Task<IActionResult> Update([FromRoute] int stockId, [FromRoute] int commentId, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var commentResponseDto = await commentService.Update(stockId, commentId, updateCommentDto);
            if (commentResponseDto == null)
            {
                return NotFound();
            }
            return Ok(commentResponseDto);

        }

        [HttpDelete("{stockId:int}/{commentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int stockId, [FromRoute] int commentId) {

            await commentService.Delete(stockId,commentId);
            return NoContent();
        }




    }
}