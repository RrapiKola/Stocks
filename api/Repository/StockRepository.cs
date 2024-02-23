using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StockDtos;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDbContext context;

        public StockRepository(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await context.Stock.AddAsync(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await context.Stock.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null){
                return null;
            }
            context.Stock.Remove(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await context.Stock.ToListAsync();
            
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await context.Stock.FindAsync(id);
        }

        public async Task<Stock?> UpdatAsync(int id, UpdateStockRequestDto updateStockRequestDto)
        {
            var stockModel= await context.Stock.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null){
                return null;
            }

            stockModel.Symbol = updateStockRequestDto.Symbol;
            stockModel.CompanyName = updateStockRequestDto.CompanyName;
            stockModel.Purchase = updateStockRequestDto.Purchase;
            stockModel.MarketCap = updateStockRequestDto.MarketCap;
            stockModel.LastDiv = updateStockRequestDto.LastDiv;
            stockModel.Industry = updateStockRequestDto.Industry;
            await context.SaveChangesAsync();

            return stockModel;
        }
    }
}