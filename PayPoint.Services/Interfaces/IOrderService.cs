using PayPoint.Core.DTOs.Orders;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface IOrderService
{
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order?> AddOrderAsync(OrderCreateDto orderCreateDto);
    Task<OrderDetail?> AddUpdateOrderDetailAsync(int orderId, IEnumerable<OrderDetailDto> orderDetails);
    Task<bool?> UpdateOrderAsync(int orderId, OrderUpdateDto orderUpdateDto);
    Task<bool?> DeleteOrderAsync(int orderId);
    
    Task<IEnumerable<OrderStatus>> GetOrderStatusAsync();
    Task<OrderStatus?> GetOrderStatusByIdAsync(int statusId);
    Task<OrderStatus?> AddOrderStatusAsync(OrderStatusCreateDto orderStatusCreateDto);
    Task<bool?> UpdateOrderStatusAsync(int statusId, OrderStatusCreateDto orderStatusUpdateDto);
    Task<bool?> DeleteOrderStatusAsync(int statusId);
}