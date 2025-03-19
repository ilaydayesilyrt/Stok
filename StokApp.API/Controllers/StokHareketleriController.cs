using App.Services.StokHareketleri;
using App.Services.StokHareketleri.DTO;
using Microsoft.AspNetCore.Mvc;
namespace App.API.Controllers
{
    public class StokHareketleriController(IStokHareketService stokHareketService) : CustomBaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockMovementById(Guid id) => CreateActionResult(await stokHareketService.GetStockMovementByIdAsync(id));
        [HttpGet]
        public async Task<IActionResult> GetStockMovements() => CreateActionResult(await stokHareketService.GetStockMovementsAsync());
        [HttpPut]
        public async Task<IActionResult> UpdateStockMovement(UpdateStokHareketRequest request) => CreateActionResult(await stokHareketService.UpdateStokHareketAsync(request));
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockMovement(Guid id) => CreateActionResult(await stokHareketService.DeleteStockMovementAsync(id));
        [HttpGet("total/{malKodu}")]
        public async Task<IActionResult> GetTotalStock(string malKodu) => CreateActionResult(await stokHareketService.GetToplamStokMiktarı(malKodu));
        [HttpPost]
        public async Task<IActionResult> CreateStokHareket([FromBody] CreateStokHareketRequest request) => CreateActionResult(await stokHareketService.CreateStokHareketAsync(request));
    }
}
