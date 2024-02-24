using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using api.Data;

using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository
    {

        private readonly ApplicationDbContext context;
        public StockRepository stockRepository;

        public CommentRepository(ApplicationDbContext context, StockRepository stockRepository)
        {
            this.stockRepository = stockRepository;
            this.context = context;

        }


        public async Task<List<Comment>> FindAll(int stockId)
        {
            var stockModel = await stockRepository.FindById(stockId);
            if (stockModel == null)
            {
                throw new Exception("Stock not found");
            }
            var commentModel = await context.Comments.Where(c => c.StockId == stockId).ToListAsync();
            return commentModel;
        }



        public async Task<Comment?> FindById(int commentId)
        {
            var commentModel = await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (commentModel == null)
            {
                return null;
            }
            return commentModel;
        }


        public async Task<Comment> Add(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return comment;
        }


        public async Task Delete(int id)
        {
            var commentToDelete = await context.Comments.FindAsync(id);

            if (commentToDelete == null)
            {
                throw new Exception($"Comment with ID {id} not found");
            }

            context.Comments.Remove(commentToDelete);
            await context.SaveChangesAsync();
        }


        public async Task<bool> CommentExistsAsync(int id)
        {
            return await context.Comments.AnyAsync(c => c.Id == id);
        }
    }

}