using PayPoint.Core.Entities;
using PayPoint.Core.Interfaces;
using PayPoint.Infrastructure.Repositories;

namespace PayPoint.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly PayPointDbContext _context;
    
    // Repositories
    public IProductRepository Products {get; private set;}
    public ICategoryRepository Categories {get; private set;}
    public IRepository<SubCategoryEntity> SubCategories {get; private set;}
    public IRepository<IngredientEntity> Ingredients {get; private set;}

    public UnitOfWork(PayPointDbContext context) {
        _context = context;
        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
        SubCategories = new Repository<SubCategoryEntity>(_context);
        Ingredients = new Repository<IngredientEntity>(_context);
    }

    public void Dispose()
    {
        _context.Dispose(); 
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
