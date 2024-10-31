using System.Linq.Expressions;

namespace PayPoint.Core.Interfaces;

public interface IRepositoryInclude<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> AsQueryable();
    IQueryable<TEntity?> GetByIdAsQueryable(int id, string propertyKey);
    Task <TEntity?> GetByIdWithIncludeAsync(int id, params Expression<Func<TEntity, object>>[] includes);
}
