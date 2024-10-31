using PayPoint.Core.Entities;

namespace PayPoint.Core.Interfaces;

public interface ICategoryRepository : IRepository<CategoryEntity>, IRepositoryInclude<CategoryEntity>
{
    
}
