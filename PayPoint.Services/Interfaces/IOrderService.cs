using PayPoint.Core.DTOs.Orders;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface IOrderService
{
    Task<Order?> GetOrderByIdAsync(int OrderId);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order?> AddOrderAsync(OrderCreateDto OrderCreateDto);
    Task<bool?> UpdateOrderAsync(int OrderId, OrderUpdateDto OrderUpdateDto);
    Task<bool?> DeleteOrderAsync(int OrderId);
}