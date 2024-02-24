using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CommentDtos;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDto>> FindAll(int stockId);

        Task<CommentResponseDto?>FindById(int stockId, int commentId);

        Task<CommentResponseDto>Add(int stockId,CreateCommentDto createCommentDto);

        Task<CommentResponseDto> Update(int stockId,int commentId,UpdateCommentDto updateCommentDto);

        Task Delete(int stockId,int commentId);
    }
}