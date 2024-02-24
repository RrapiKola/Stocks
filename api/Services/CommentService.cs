using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.CommentDtos;
using api.Dtos.StockDtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;


namespace api.Services
{
    public class CommentService : ICommentService
    {

        private readonly CommentRepository commentRepository;
        private readonly StockRepository stockRepository;
        private readonly IStockService stockService;
        private readonly ApplicationDbContext context;

        public CommentService(CommentRepository commentRepository,
        StockRepository stockRepository,
        IStockService stockService,
        ApplicationDbContext context)
        {
            this.stockService = stockService;
            this.context = context;
            this.stockRepository = stockRepository;
            this.commentRepository = commentRepository;
        }



        public async Task<IEnumerable<CommentResponseDto>> FindAll(int stockId)
        {
            if (!await stockService.StockExists(stockId))
            {
                throw new Exception("Stock not found");
            }

            var listComment = await commentRepository.FindAll(stockId);
            var listCommentDtos = listComment.Select(c => c.MapToResponseDto());
            return listCommentDtos.ToList();
        }



        public async Task<CommentResponseDto?> FindById(int stockId, int commentId)
        {
            if (!await stockService.StockExists(stockId))
            {
                throw new Exception("Stock not found");
            }
            var commentModel = await commentRepository.FindById(commentId);
            if (commentModel?.StockId != stockId)
            {
                throw new Exception($"Comment with Id: {commentId} doesn't correspond with StockId: {stockId}");
            }
            return commentModel.MapToResponseDto();
        }



        public async Task<CommentResponseDto> Add(int stockId, CreateCommentDto createCommentDto)
        {

            if (!await stockService.StockExists(stockId))
            {
                throw new Exception("Stock not found");
            }
            var commentModel = createCommentDto.MapCreateCommentToCommentModel();
            commentModel.StockId = stockId;
            await commentRepository.Add(commentModel);
            return commentModel.MapToResponseDto();
        }

        

        public async Task<CommentResponseDto> Update(int stockId, int commentId, UpdateCommentDto updateComment)
        {

            if (!await stockService.StockExists(stockId))
            {
                throw new Exception("Stock not found");
            }
            if (!await commentRepository.CommentExistsAsync(commentId))
            {
                throw new Exception("Comment not found");
            }

            var commentModel = await commentRepository.FindById(commentId);

            if (commentModel?.StockId != stockId)
            {
                throw new Exception($"Comment with Id: {commentId} doesn't correspond with StockId: {stockId}");
            }

            commentModel.Title = updateComment.Title;
            commentModel.Content = updateComment.Content;
            commentModel.StockId = updateComment.StockId;
            await context.SaveChangesAsync();

            return commentModel.MapToResponseDto();
        }


        public async Task Delete(int stockId, int commentId)
        {

            if (!await stockService.StockExists(stockId))
            {
                throw new Exception("Stock not found");
            }
            if (!await commentRepository.CommentExistsAsync(commentId))
            {
                throw new Exception("Comment not found");
            }

            var commentModel = await commentRepository.FindById(commentId);

            if (commentModel?.StockId != stockId)
            {
                throw new Exception($"Comment with Id: {commentId} doesn't correspond with StockId: {stockId}");
            }

            await commentRepository.Delete(commentId);

        }


    }
}