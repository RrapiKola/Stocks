using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StockDtos;
using api.Models;
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



        public async Task<List<Stock>> GetAllAsync()
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



        // public async Task<Stock?> UpdatAsync(int id, UpdateStockDto updateStockDto)
        // {
        //     var stockModel = await context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        //     if (stockModel == null)
        //     {
        //         return null;
        //     }

        //     stockModel.Symbol = updateStockDto.Symbol;
        //     stockModel.CompanyName = updateStockDto.CompanyName;
        //     stockModel.Purchase = updateStockDto.Purchase;
        //     stockModel.MarketCap = updateStockDto.MarketCap;
        //     stockModel.LastDiv = updateStockDto.LastDiv;
        //     stockModel.Industry = updateStockDto.Industry;
        //     await context.SaveChangesAsync();

        //     return stockModel;
        // }
    }
}