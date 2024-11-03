using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.Interfaces;

namespace PayPoint.Infrastructure.Data;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly PayPointDbContext _context;
    private DbSet<TEntity> _dbSet;

    public Repository(PayPointDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity is not null)
        {
            _dbSet.Remove(entity);
        }
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public IQueryable<TEntity?> GetByIdAsQueryable(int id, string propertyKey)
    {
        var query = _dbSet.AsQueryable();
        query = query.Where(entity => EF.Property<int>(entity, propertyKey) == id);

        return query;
    }

    public async Task<TRequest?> GetByIdWithIncludeAsync<TRequest>(int id, params Expression<Func<TRequest, object>>[] includes)
        where TRequest : class, IEntity
    {
        var query = _context.Set<TRequest>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
}
