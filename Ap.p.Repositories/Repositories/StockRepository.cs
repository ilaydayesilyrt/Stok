using App.Repositories.Entities;
using App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace App.Repositories.Repositories
{
    public class StockRepository(AppDbContext context) : GenericRepository<Stock>(context), IStockRepository
    {
        public async Task AddStockAsync(Stock stock)
        {
            await context.AddAsync(stock);
            await context.SaveChangesAsync();
        }
        public async Task DeleteStockAsync(Guid stockId)
        {
            var stock=await context.Stocks.FirstOrDefaultAsync(x=>x.Id==stockId);
            context.Stocks.Remove(stock!);
            await context.SaveChangesAsync();
        }
        public async Task<List<Stock>> GetAllStocksAsync()
        {
           return await context.Stocks.ToListAsync();
        }
        public async Task<Stock?> GetStockById(Guid stockId)
        {
            return await context.Stocks.FindAsync(stockId);
        }
        public async Task UpdateStockAsync(Stock stock)
        {
            context.Update(stock);
            await context.SaveChangesAsync();
        }
    }
}
