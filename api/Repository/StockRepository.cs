using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StockDtos;
using api.Models;
using api.Utitlities;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository 
    {
        private readonly ApplicationDbContext context;
        public StockRepository(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }




        public async Task<Stock> Add(Stock stockModel)
        {
            await context.Stock.AddAsync(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }



        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            context.Stock.Remove(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }



        public async Task<List<Stock>> FindAll()
        {
            return await context.Stock.Include(c=>c.Comments).ToListAsync();

        }



        public async Task<Stock?> FindById(int stockId)
        {
            return await context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.Id==stockId);
        }



        public async Task<bool> StockExistsAsync(int stockId)
        {
            return await context.Stock.AnyAsync(s=>s.Id==stockId);
        }

    }
}