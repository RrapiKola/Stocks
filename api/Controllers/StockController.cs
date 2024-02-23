using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StockDtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IStockRepository iStockRepository;

        public StockController(IStockRepository stockRepository,ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
            iStockRepository=stockRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await iStockRepository.GetAllAsync();
            var stockDtoReturnList = stocks.Select(s => s.ToStockDto());
            return Ok(stockDtoReturnList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await iStockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            var stockModel = createStockRequestDto.StockModelFromCreateStockRequestDto();
            await iStockRepository.CreateAsync(stockModel);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateStockRequestDto updateStockRequestDto, [FromRoute] int id)
        {
            var stockModel = await iStockRepository.UpdatAsync(id,updateStockRequestDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await iStockRepository.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}