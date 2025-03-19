using App.Repositories.Interfaces;
using App.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;
    private readonly IServiceProvider _serviceProvider;

    private IStokHareketleriRepository _stokHareketleriRepository;
    private IStockRepository _stockRepository;

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IStokHareketleriRepository StokHareketleriRepository
    {
        get
        {
            if (_stokHareketleriRepository == null)
            {
                _stokHareketleriRepository = _serviceProvider.GetRequiredService<IStokHareketleriRepository>();
            }
            return _stokHareketleriRepository;
        }
    }

    public IStockRepository StockRepository
    {
        get
        {
            if (_stockRepository == null)
            {
                _stockRepository = _serviceProvider.GetRequiredService<IStockRepository>();
            }
            return _stockRepository;
        }
    }

    public void Save() => _context.SaveChanges();

    public async Task<int> SaveAsyncWithoutValidation()
    {
        return await _context.SaveChangesAsync();
    }

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

    public void SaveWithoutValidation() => _context.SaveChanges();

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction is in progress.");
        }

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction is in progress.");
        }

        await _transaction.RollbackAsync();
        _transaction = null;
    }
}
