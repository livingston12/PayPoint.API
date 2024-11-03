using PayPoint.Core.Entities;

namespace PayPoint.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
   IProductRepository Products { get; }
   ICategoryRepository Categories { get; }
   IRepository<SubCategoryEntity> SubCategories { get; }
   IRepository<IngredientEntity> Ingredients { get; }
   Task<int> SaveChangesAsync();
}
