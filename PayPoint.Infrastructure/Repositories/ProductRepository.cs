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

    /// <summary>
    /// Gets all products by given sub category id.
    /// </summary>
    /// <param name="categoryId">category id.</param>
    /// <returns>Products by sub category id.</returns>
    public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .Include(x => x.SubCategory!.Category)
            .Where(x => x.SubCategory!.CategoryId == categoryId)
            .ToListAsync();
    }
}
