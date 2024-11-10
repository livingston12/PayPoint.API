using PayPoint.Core.Entities;
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

    public Task AddDetailAsync(OrderDetailEntity orderDetailEntity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDetailAsync(int detailId)
    {
        throw new NotImplementedException();
    }

    public void UpdateDetail(OrderDetailEntity orderDetailEntity)
    {
        throw new NotImplementedException();
    }
}
