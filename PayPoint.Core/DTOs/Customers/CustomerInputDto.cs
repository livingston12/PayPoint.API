namespace PayPoint.Core.DTOs.Customers;

public class CustomerInputDto
{
    public bool? IncludeAll { get; set; }
    public bool? IncludePaymentPreference { get; set; }
    public bool? IncludeInvoices { get; set; }
}
