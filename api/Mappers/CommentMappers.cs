using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CommentDtos;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {


        public static CommentResponseDto MapToResponseDto(this Comment comment)
        {
            return new CommentResponseDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId 
            
            };
        }

        public static Comment MapCreateCommentToCommentModel(this CreateCommentDto createCommentDto) {
            return new Comment{
                Title = createCommentDto.Title,
                Content = createCommentDto.Content
            };
        }


        public static Comment MapUpdateToModel(this UpdateCommentDto updateCommentDto) {
            return new Comment{
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content
            };
        }
    }
}