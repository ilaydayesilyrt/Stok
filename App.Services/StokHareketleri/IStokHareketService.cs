using App.Services.StokHareketleri.DTO;
namespace App.Services.StokHareketleri
{
    public interface IStokHareketService
    {
        Task<ServiceResult<List<StokHareketDto>>> GetStockMovementsAsync();
        Task<ServiceResult<StokHareketDto>> GetStockMovementByIdAsync(Guid stockHarekettId);
        Task<ServiceResult<CreateStokHareketResponse>> CreateStokHareketAsync(CreateStokHareketRequest request);
        Task<ServiceResult<UpdateStokHareketRequest>> UpdateStokHareketAsync(UpdateStokHareketRequest request);
        Task<ServiceResult<int>> GetToplamStokMiktarı(string malKodu);
        Task<ServiceResult> DeleteStockMovementAsync(Guid stockHarekettId);
    }
}
