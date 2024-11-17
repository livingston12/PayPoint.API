using PayPoint.Core.Entities;

namespace PayPoint.Core.Interfaces;

public interface IOrderRepository : IRepository<OrderEntity>
{
    Task AddDetailAsync(OrderDetailEntity orderDetailEntity);
    void UpdateDetail(OrderDetailEntity orderDetailEntity);
    Task DeleteDetailAsync(int orderId, int productId);
    IQueryable<OrderDetailEntity> DetailAsQueryable();
}
