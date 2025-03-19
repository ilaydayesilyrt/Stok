using App.Services.Stocks.DTO;

namespace App.Services.Stocks
{
    public interface IStockService
    {
        Task<ServiceResult<CreateStockResponse>> CreateStockAsync(CreateStockRequest request);
        Task<ServiceResult> UpdateStockAsync( UpdateStockRequest request);
        Task<ServiceResult> DeleteStockAsync(Guid id);
    }
}
