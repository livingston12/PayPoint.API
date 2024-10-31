namespace PayPoint.Core.Entities;

public class PaymentMethodEntity : BaseEntity
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public ICollection<CustomerEntity>? Customers { get; set; }
    public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
}
