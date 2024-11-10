namespace PayPoint.Core.Models;

public class Order
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public int UserId { get; set; }
    public int? CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public int OrderStatusId { get; set; }
}