using PayPoint.Core.DTOs.Customers;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int CustomerId);
    Task<IEnumerable<Customer>> GetCustomersAsync(CustomerInputDto CustomerDto);
    Task<Customer?> AddCustomerAsync(CustomerCreateDto CustomerCreateDto);
    Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto CustomerUpdateDto);
    Task<bool> DeleteCustomerAsync(int id);
}
