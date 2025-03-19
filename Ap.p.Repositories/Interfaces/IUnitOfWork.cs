namespace App.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
       Task<int> SaveChangesAsync();
       Task<int> SaveAsyncWithoutValidation();
       void Save();
       void SaveWithoutValidation();

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        IStokHareketleriRepository StokHareketleriRepository { get; }
        IStockRepository StockRepository { get; }
    }
}
