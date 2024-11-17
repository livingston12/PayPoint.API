namespace PayPoint.Core.DTOs.Orders;

public class OrderUpdateDto
{
    public int TableId { get; set; }
    public int UserId { get; set; }
    public int? CustomerId { get; set; }
    public int OrderStatusId { get; set; }
    public IEnumerable<OrderDetailDto>? OrderDetails { get; set; }
}
