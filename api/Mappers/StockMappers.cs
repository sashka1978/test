using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using api.DTOs;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto toStocksDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c=>c.toCommentsDto()).ToList()
            };
        }
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,                
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                Industry = createStockRequestDto.Industry,
                MarketCap = createStockRequestDto.MarketCap

            };
        }
         public static Stock ToStockFromFMP(this FMPStock fMPStock)
        {
            return new Stock
            {
                Symbol = fMPStock.symbol,
                CompanyName = fMPStock.companyName,                
                Purchase = (decimal)fMPStock.price,
                LastDiv = (decimal)fMPStock.lastDiv,
                Industry = fMPStock.industry,
                MarketCap = fMPStock.mktCap

            };
        }
    }
}