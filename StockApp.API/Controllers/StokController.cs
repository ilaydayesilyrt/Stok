using App.Services.Stocks;
using App.Services.Stocks.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StockApp.API.Controllers
{
    public class StokController(IStockService stockService) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequest request) => CreateActionResult(await stockService.CreateStockAsync(request));
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(Guid id) => CreateActionResult(await stockService.DeleteStockAsync(id));
        [HttpPut]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequest request) => CreateActionResult(await stockService.UpdateStockAsync(request));
    }
    
}
