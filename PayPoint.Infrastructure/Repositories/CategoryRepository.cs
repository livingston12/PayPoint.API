using System.Linq.Expressions;
using PayPoint.Core.Entities;
using PayPoint.Core.Interfaces;
using PayPoint.Infrastructure.Data;

namespace PayPoint.Infrastructure.Repositories;

public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
{

    public CategoryRepository(PayPointDbContext context) : base(context)
    {
    }

    public Task<CategoryEntity?> GetByIdWithIncludeAsync(int id, params Expression<Func<CategoryEntity, object>>[] includes)
    {
        return GetByIdWithIncludeAsync<CategoryEntity>(id, includes);
    }
}
