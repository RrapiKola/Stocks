using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StockDtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using api.Utitlities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class StockService : IStockService
    {
        private readonly StockRepository stockRepository;
        private readonly ApplicationDbContext context;

        public StockService(StockRepository stockRepository, ApplicationDbContext context)
        {
            this.context = context;
            this.stockRepository = stockRepository;
        }


        public async Task<StockDto> Add(CreateStockDto stockDto)
        {
            var stockModel = stockDto.MapToModel();
            await stockRepository.Add(stockModel);
            return stockModel.ToStockDto();
        }


        public async Task<StockDto?> FindById(int stockId)
        {
            var stockModel = await stockRepository.FindById(stockId);
            if (stockModel == null)
            {
                return null;
            }
            return stockModel.ToStockDto();
        }


        public async Task<IEnumerable<StockDto>> FindAll()
        {
            var listOfStocks = await stockRepository.FindAll();
            var listOfStockDtos = listOfStocks.Select(stock => stock.ToStockDto());
            return listOfStockDtos.ToList();
        }

        public async Task<List<StockDto>> FindAllByQuery(QueryObject queryObject)
        {
            var listOfStocks = await stockRepository.FindAll();

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                listOfStocks = listOfStocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                listOfStocks = listOfStocks.Where(s => s.Symbol.Contains(queryObject.Symbol)).ToList();
            }

            return  listOfStocks.Select(stock => stock.ToStockDto()).ToList();
        }


        public async Task<StockDto?> Update(int stockId, UpdateStockDto updateStockDto)
        {
            var stockModel = await context.Stock.FirstOrDefaultAsync(x => x.Id == stockId);
            if (stockModel == null)
            {
                return null;
            }

            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.CompanyName = updateStockDto.CompanyName;
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.MarketCap = updateStockDto.MarketCap;
            stockModel.LastDiv = updateStockDto.LastDiv;
            stockModel.Industry = updateStockDto.Industry;
            await context.SaveChangesAsync();

            return stockModel?.ToStockDto();
        }


        public async Task<StockDto?> Delete(int id)
        {
            var stockModel = await stockRepository.DeleteAsync(id);
            return stockModel != null ? stockModel.ToStockDto() : null;
        }



        public async Task<bool> StockExists(int id)
        {
            bool exists = await stockRepository.StockExistsAsync(id);
            return exists;
        }


    }
}