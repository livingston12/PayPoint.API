using Microsoft.EntityFrameworkCore.Storage;
using PayPoint.Core.Entities;
using PayPoint.Core.Interfaces;
using PayPoint.Infrastructure.Repositories;

namespace PayPoint.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly PayPointDbContext _context;
    private IDbContextTransaction? _transaction;

    // Repositories
    public IProductRepository Products { get; private set; }
    public ICategoryRepository Categories { get; private set; }
    public IOrderRepository Orders { get; private set; }
    public IRepository<SubCategoryEntity> SubCategories { get; private set; }
    public IRepository<IngredientEntity> Ingredients { get; private set; }
    public IRepository<TableEntity> Tables { get; private set; }
    public IRepository<RoomEntity> Rooms { get; private set; }
    public IRepository<CustomerEntity> Customers { get; private set; }
    public IRepository<UserEntity> Users { get; private set; }

    public UnitOfWork(PayPointDbContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
        SubCategories = new Repository<SubCategoryEntity>(_context);
        Ingredients = new Repository<IngredientEntity>(_context);
        Tables = new Repository<TableEntity>(_context);
        Rooms = new Repository<RoomEntity>(_context);
        Orders = new OrderRepository(_context);
        Customers = new Repository<CustomerEntity>(_context);
        Users = new Repository<UserEntity>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
