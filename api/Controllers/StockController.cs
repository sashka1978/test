using api.Data;
using api.DTOs;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly ApplicationDBContext _context;
       public StockController(ApplicationDBContext context, IStockRepository stockRepo)
       {
            _stockRepo = stockRepo;
            _context = context;
       }
       [HttpGet]
       [Authorize]
       public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
       {
         if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetAllasync(query);

            var stockDto = stocks.Select(s=>s.toStocksDto()).ToList();

            return Ok(stockDto);
       }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.toStocksDto());
        }
        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] CreateStockRequestDto stockDto)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new{ id = stockModel.Id}, stockModel.toStocksDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute]int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel= await _stockRepo.UpdateAsync(id, updateStockRequestDto);
            if(stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.toStocksDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute]int id)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel= await _stockRepo.DeleteAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            }           
           
            return NoContent();
        }
    }
}