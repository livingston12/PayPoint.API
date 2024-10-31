using PayPoint.Core.Enums;

namespace PayPoint.Core.Entities;

public class PaymentEntity
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public PaymentStatus Estatus { get; set; }

    public OrderEntity Order { get; set; }
    public PaymentMethodEntity PaymentMethod { get; set; }

}
