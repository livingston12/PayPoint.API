using PayPoint.Core.Entities;

namespace PayPoint.Core.Interfaces;

public interface IOrderRepository : IRepository<OrderEntity>
{
    Task<IEnumerable<OrderStatusEntity>?> GetAllStatusAsync();
    Task AddDetailAsync(OrderDetailEntity orderDetailEntity);
    void UpdateDetail(OrderDetailEntity orderDetailEntity);
    Task DeleteDetailAsync(int orderId, int productId);
}
