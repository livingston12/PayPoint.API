using Microsoft.EntityFrameworkCore;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Infrastructure.Data;

namespace PayPoint.Infrastructure.Repositories;

public class OrderRepository : Repository<OrderEntity>, IOrderRepository
{
    private readonly PayPointDbContext _context;

    public OrderRepository(PayPointDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddDetailAsync(OrderDetailEntity orderDetailEntity)
    {
        await _context.OrderDetails.AddAsync(orderDetailEntity);
    }

    public async Task DeleteDetailAsync(int orderId, int productId)
    {
        OrderDetailEntity? orderDetailEntity = await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);

        if (orderDetailEntity.IsNotNullOrEmpty())
        {
            _context.OrderDetails.Remove(orderDetailEntity!);
        }
    }

    public async Task<IEnumerable<OrderStatusEntity>?> GetAllStatusAsync()
    {
        return await _context.OrderStatus.ToListAsync();
    }

    public void UpdateDetail(OrderDetailEntity orderDetailEntity)
    {
        _context.Update(orderDetailEntity);
    }
}
