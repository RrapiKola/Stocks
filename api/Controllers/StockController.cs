using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StockDtos;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService stockService;
        public StockController(StockService stockService)
        {
            this.stockService = stockService;
        }




        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var stockDtoReturnList = await stockService.FindAll();
            return Ok(stockDtoReturnList);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] int id)
        {
            var stockDto = await stockService.FindById(id);
            if (stockDto == null)
            {
                return NotFound();
            }
            return Ok(stockDto);
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockRequestDto)
        {
            var createdStockDto = await stockService.Add(createStockRequestDto);
            return CreatedAtAction(nameof(FindById), new { id = createdStockDto.Id }, createdStockDto);
        }





        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateStockDto updateStockDto, [FromRoute] int id)
        {
           var stockDto = await stockService.Update(id,updateStockDto);
           return Ok(stockDto);
        }




        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await stockService.Delete(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}