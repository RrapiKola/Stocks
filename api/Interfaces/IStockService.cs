using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StockDtos;
using api.Models;

namespace api.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<StockDto>> FindAll();
        Task<StockDto?>FindById(int id);
        Task<StockDto>Add(CreateStockDto createStockDto);
        Task<StockDto?>Update(int id,UpdateStockDto updateStockDto);
        Task<StockDto?>Delete(int id);
        Task<bool> StockExists(int id); 
    }
}