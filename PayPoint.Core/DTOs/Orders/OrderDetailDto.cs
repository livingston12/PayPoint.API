namespace PayPoint.Core.DTOs.Orders;

public class OrderDetailDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal? Discount { get; set; }
}
