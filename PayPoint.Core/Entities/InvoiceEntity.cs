namespace PayPoint.Core.Entities;

public class InvoiceEntity
{
    public int InvoiceId { get; set; }
    public int? CustomerId { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public DateTime EmisionDate { get; set; }
    public string Detail { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? Discount { get; set; }
    public decimal? Tax { get; set; }

    public CustomerEntity? Customer { get; set; }
    public UserEntity User { get; set; }
    public OrderEntity Order { get; set; }
}
