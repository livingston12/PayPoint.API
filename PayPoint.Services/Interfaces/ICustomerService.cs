using PayPoint.Core.DTOs.Customers;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int customerId, CustomerInputDto customerDto);
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<Customer?> AddCustomerAsync(CustomerCreateDto customerCreateDto);
    Task<bool?> UpdateCustomerAsync(int customerId, CustomerUpdateDto customerUpdateDto);
    Task<bool?> DeleteCustomerAsync(int customerId);
}
