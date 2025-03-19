using App.Repositories.Entities;
namespace App.Repositories.Interfaces
{
    public interface IStokHareketleriRepository : IGenericRepository<StokHareket>
    {
        Task<List<StokHareket>> GetAllAsync();
        Task<decimal> SumMimktar();
        Task UpdateStokHareketAsync(StokHareket stokHareket);
        Task AddStokHareketAsync(StokHareket stokHareket);
        Task DeleteStokHareketAsync(Guid stokHareketId);
    }
}
