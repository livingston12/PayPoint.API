namespace PayPoint.Core.Entities;

public class OrderEntity
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public int UserId { get; set; }
    public int? CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public OrderStatusEntity Status { get; set; } = new();    
    public UserEntity User { get; set; }
    public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
}
