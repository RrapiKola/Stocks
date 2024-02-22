using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StockDtos;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public StockController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var stockDtoList = context.Stock.ToList().Select(s=>s.ToStockDto());
            return Ok(stockDtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute]int id){
            var stock = context.Stock.Find(id);
            if(stock==null){
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
       
    }
}