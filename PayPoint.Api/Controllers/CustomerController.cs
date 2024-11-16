using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Customers;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        IEnumerable<Customer> customers = await _customerService.GetCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id,
        [FromHeader] bool? includeAll,
        [FromQuery] bool? includeInvoices,
        [FromQuery] bool? IncludePayment)
    {
        var customerInput = new CustomerInputDto()
        {
            IncludeAll = includeAll == true,
            IncludeInvoices = includeInvoices == true,
            IncludePaymentPreference = IncludePayment == true
        };

        Customer? customer = await _customerService.GetCustomerByIdAsync(id, customerInput);

        if (customer.IsNullOrEmpty())
        {
            return NotFound("No se encontro el cliente.");
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CustomerCreateDto customerCreateDto)
    {
        Customer? customer = await _customerService.AddCustomerAsync(customerCreateDto);

        if (customer.IsNullOrEmpty())
        {
            return BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
        }

        return CreatedAtAction(nameof(GetCustomerById), new { id = customer!.CustomerId }, customer);
    }
}
