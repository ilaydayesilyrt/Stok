using App.Repositories.Entities;
using App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace App.Repositories.Repositories
{
    public class StokHareketleriRepository(AppDbContext context) : GenericRepository<StokHareket>(context), IStokHareketleriRepository
    {
        public async Task AddStokHareketAsync(StokHareket stokHareket)
        {
            await context.AddAsync(stokHareket);
            await context.SaveChangesAsync();
        }
        public async Task DeleteStokHareketAsync(Guid stokHareketId)
        {
            var stokHareket = await context.StokHareketleri.FirstOrDefaultAsync(x => x.Id == stokHareketId);
            context.StokHareketleri.Remove(stokHareket!);
            await context.SaveChangesAsync();
        }
        public async Task<List<StokHareket>> GetAllAsync()
        {
            return await context.StokHareketleri.ToListAsync();
        }
        public async Task<decimal> SumMimktar()
        {
            return await context.StokHareketleri.SumAsync(s=>s.Miktar);
        }
        public async Task UpdateStokHareketAsync(StokHareket stokHareket)
        {
            context.Update(stokHareket);
            await context.SaveChangesAsync();
        }
    }
}