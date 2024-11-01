namespace PayPoint.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity?>> GetAllAsync();
    IQueryable<TEntity> AsQueryable();
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task DeleteAsync(int id);
}
