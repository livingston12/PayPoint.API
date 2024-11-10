using AutoMapper;
using PayPoint.Core.DTOs.Customers;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class CustomerService : BaseService, ICustomerService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Customer?> AddCustomerAsync(CustomerCreateDto CustomerCreateDto)
    {
        CustomerEntity customerEntity = _mapper.Map<CustomerEntity>(CustomerCreateDto);

        await _unitOfWork.Customers.AddAsync(customerEntity);
        int? rowsInserted = await _unitOfWork.SaveChangesAsync();

        if (rowsInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Customer?>(customerEntity);
    }

    public Task<bool> DeleteCustomerAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerByIdAsync(int CustomerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetCustomersAsync(CustomerInputDto CustomerDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto CustomerUpdateDto)
    {
        throw new NotImplementedException();
    }
}
