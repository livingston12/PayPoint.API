using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.DTOs.Customers;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Infrastructure.Extensions;
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

    public async Task<Customer?> AddCustomerAsync(CustomerCreateDto customerCreateDto)
    {
        CustomerEntity customerEntity = _mapper.Map<CustomerEntity>(customerCreateDto);

        await _unitOfWork.Customers.AddAsync(customerEntity);
        int? rowsInserted = await _unitOfWork.SaveChangesAsync();

        if (rowsInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Customer?>(customerEntity);
    }

    public async Task<bool?> DeleteCustomerAsync(int customerId)
    {
        CustomerEntity? customerEntity = await _unitOfWork.Customers.GetByIdAsync(customerId);

        if (customerEntity.IsNullOrEmpty())
        {
            return null;
        }

        await _unitOfWork.Customers.DeleteAsync(customerId);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId, CustomerInputDto customerDto)
    {
        var query = _unitOfWork.Customers.AsQueryable();

        query = IncludeValues(query, customerDto);

        CustomerEntity? customerEntity = await query.FirstOrDefaultAsync(x => x.CustomerId == customerId);

        if (customerEntity.IsNullOrEmpty())
        {
            return null;
        }

        return _mapper.Map<Customer>(customerEntity);
    }

    private IQueryable<CustomerEntity> IncludeValues(IQueryable<CustomerEntity> query, CustomerInputDto customerDto)
    {
        if (customerDto.IncludeAll == true)
        {
            return query.IncludeAll();
        }

        if (customerDto.IncludeInvoices == true)
        {
            query = query.Include(x => x.Invoices);
        }

        if (customerDto.IncludePaymentPreference == true)
        {
            query = query.Include(x => x.PaymentPreference);
        }

        return query;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        IEnumerable<CustomerEntity>? customersEntity = await _unitOfWork.Customers.GetAllAsync();

        return _mapper.Map<IEnumerable<Customer>>(customersEntity);
    }

    public async Task<bool?> UpdateCustomerAsync(int customerId, CustomerUpdateDto customerUpdateDto)
    {
        CustomerEntity? customerEntity = await _unitOfWork.Customers.GetByIdAsync(customerId);

        if (customerEntity.IsNullOrEmpty())
        {
            return null;
        }

        customerEntity = _mapper.Map(customerUpdateDto, customerEntity);

        _unitOfWork.Customers.Update(customerEntity!);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);
    }
}
