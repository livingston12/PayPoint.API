using PayPoint.Core.Entities;

namespace PayPoint.Core.Interfaces;

public interface IProductRepository : IRepository<ProductEntity>, IRepositoryInclude<ProductEntity>
{
    //Task <ProductEntity?> GetByIdWithIncludeAsync(int id, params Expression<Func<ProductEntity, object>>[] includes);
    Task<IEnumerable<ProductEntity>> GetProductsBySubCategoryIdAsync(int categoryId);
}
