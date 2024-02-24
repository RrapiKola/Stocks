using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StockDtos;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel){

            return new StockDto{
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                CommentResponseDtos = stockModel.Comments.Select(c => c.MapToResponseDto()).ToList()
            };

        }


        public static Stock MapToModel(this CreateStockDto createStockRequestDto  ) {
            return new Stock{

                Symbol=createStockRequestDto.Symbol,
                CompanyName=createStockRequestDto.CompanyName,
                Purchase=createStockRequestDto.Purchase,
                LastDiv=createStockRequestDto.LastDiv,
                Industry=createStockRequestDto.Industry,
                MarketCap=createStockRequestDto.MarketCap
            };


        }
    }
}