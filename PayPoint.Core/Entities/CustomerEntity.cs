namespace PayPoint.Core.Entities;

public class CustomerEntity : BaseEntity
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }

    public int? PaymentPreferenceId { get; set; }
    public PaymentMethodEntity? PaymentPreference { get; set; }
}
