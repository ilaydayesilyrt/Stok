using App.Repositories.Entities;
namespace App.Repositories.Interfaces
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task UpdateStockAsync(Stock stock);
        Task AddStockAsync(Stock stock);
        Task DeleteStockAsync(Guid stockId);
        Task<Stock?> GetStockById(Guid stockId);
    }
}
