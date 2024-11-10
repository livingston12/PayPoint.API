using PayPoint.Core.Entities;

namespace PayPoint.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
   IProductRepository Products { get; }
   ICategoryRepository Categories { get; }
   IOrderRepository Orders { get; }
   IRepository<SubCategoryEntity> SubCategories { get; }
   IRepository<IngredientEntity> Ingredients { get; }
   IRepository<TableEntity> Tables { get; }
   IRepository<RoomEntity> Rooms { get; }
   IRepository<CustomerEntity> Customers { get; }
   IRepository<UserEntity> Users { get; }

   Task<int> SaveChangesAsync();
   Task BeginTransactionAsync();
   Task CommitTransactionAsync();
   Task RollbackTransactionAsync();
}
