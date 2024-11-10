namespace PayPoint.Core.Entities;

public class OrderEntity
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public int UserId { get; set; }
    public int? CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public int OrderStatusId { get; set; }
    
    public OrderStatusEntity? Status { get; set; }
    public UserEntity? User { get; set; }
    public TableEntity? Table { get; set; }
    public CustomerEntity? Customer { get; set; }
    public ICollection<PaymentEntity>? Payments { get; set; }
    public ICollection<InvoiceEntity>? Invoices { get; set; }
    public ICollection<OrderDetailEntity>? OrderDetails { get; set; }
}
