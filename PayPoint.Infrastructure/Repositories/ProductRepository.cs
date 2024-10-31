using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.Entities;
using PayPoint.Core.Interfaces;
using PayPoint.Infrastructure.Data;

namespace PayPoint.Infrastructure.Repositories;

public class ProductRepository : Repository<ProductEntity>, IProductRepository
{
    private readonly PayPointDbContext _context;

    public ProductRepository(PayPointDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<ProductEntity?> GetByIdWithIncludeAsync(int id, params Expression<Func<ProductEntity, object>>[] includes)
    {
        return await GetByIdWithIncludeAsync<ProductEntity>(id, includes);
    }

    /// <summary>
    /// Gets all products by given sub category id.
    /// </summary>
    /// <param name="subCategoryId">Sub category id.</param>
    /// <returns>Products by sub category id.</returns>
    public async Task<IEnumerable<ProductEntity>> GetProductsBySubCategoryIdAsync(int subCategoryId)
    {
        return await _context.Products
            .Where(x => x.SubCategoryId == subCategoryId)
            .Include(x => x.SubCategory)
            .ToListAsync();
    }
}
