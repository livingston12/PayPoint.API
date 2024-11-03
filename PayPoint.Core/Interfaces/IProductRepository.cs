using PayPoint.Core.Entities;

namespace PayPoint.Core.Interfaces;

public interface IProductRepository : IRepository<ProductEntity>
{
    Task<IEnumerable<ProductEntity>> GetProductsByCategoryIdAsync(int categoryId);
}
