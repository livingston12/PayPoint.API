using PayPoint.Core.DTOs.Orders;

namespace PayPoint.Core.Models;

public class OrderDetail
{
    public int OrderId { get; set; }
    public IEnumerable<OrderDetailDto>? Details { get; set; }
}
