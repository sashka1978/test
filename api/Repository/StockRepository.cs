using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
             _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel == null)
            {
                return null;
            }
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }      

        public async Task<List<Stock>> GetAllasync(QueryObject query)
        {
            var stocks= _context.Stock.Include(c=>c.Comments).ThenInclude(a => a.AppUser).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }                
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.Id ==id);
             
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stock.FirstOrDefaultAsync(x=>x.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockRequestDto)
        {
           var existingStock = await _context.Stock.FirstOrDefaultAsync(x=>x.Id==id);
           if(existingStock == null)
           {
                return null;
           }
            existingStock.Symbol = stockRequestDto.Symbol;
            existingStock.CompanyName = stockRequestDto.CompanyName;
            existingStock.Purchase = stockRequestDto.Purchase;
            existingStock.LastDiv = stockRequestDto.LastDiv; 
            existingStock.Industry = stockRequestDto.Industry;
            existingStock.MarketCap = stockRequestDto.MarketCap;    
            await _context.SaveChangesAsync();
            return existingStock;
        }
      
    }
}