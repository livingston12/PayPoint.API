namespace PayPoint.Core.Entities;

// TODO: Add userIdentity
public class UserEntity
{
    public int UserId { get; set; }
    public string FirtName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime? HireDate { get; set; }
    public decimal? Salary { get; set; }
    public string? Turn { get; set; }

    public ICollection<OrderEntity>? Orders { get; set; }
    public ICollection<InvoiceEntity>? Invoices { get; set; }
}
