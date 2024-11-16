using AutoMapper;
using PayPoint.Core.DTOs.Orders;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class OrderService : BaseService, IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Order?> AddOrderAsync(OrderCreateDto OrderCreateDto)
    {
        bool hasOrderDetails = OrderCreateDto.OrderDetails?.Any() == true;

        if (hasOrderDetails)
        {
            await _unitOfWork.BeginTransactionAsync();
        }

        try
        {
            OrderEntity orderEntity = _mapper.Map<OrderEntity>(OrderCreateDto);

            if (orderEntity.IsNullOrEmpty())
            {
                return null;
            }

            await _unitOfWork.Orders.AddAsync(orderEntity);
            int? rowInserted = await _unitOfWork.SaveChangesAsync();

            if (rowInserted.IsLessThanOrEqualTo(0))
            {
                return null;
            }

            // Insert product to order.
            if (hasOrderDetails)
            {
                await AddOrderDetailsToOrder(orderEntity, OrderCreateDto.OrderDetails!.ToList());

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }

            return _mapper.Map<Order>(orderEntity);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return null;
        }
    }

    private async Task AddOrderDetailsToOrder(OrderEntity orderEntity, List<OrderDetailDto> orderDetails)
    {
        orderEntity.OrderDetails = new List<OrderDetailEntity>();

        foreach (OrderDetailDto orderDetail in orderDetails)
        {
            ProductEntity? productExist = await _unitOfWork.Products.GetByIdAsync(orderDetail.ProductId);

            if (productExist.IsNotNullOrEmpty())
            {
                continue;
            }

            OrderDetailEntity orderDetailEntity = _mapper.Map<OrderDetailEntity>(orderDetail);
            orderDetailEntity.OrderId = orderEntity.OrderId;
            orderDetailEntity.UnitPrice = productExist!.Price;

            await _unitOfWork.Orders.AddDetailAsync(orderDetailEntity);
            orderEntity.OrderDetails.Add(orderDetailEntity);
        }
    }

    public async Task<bool?> DeleteOrderAsync(int orderId)
    {
        OrderEntity? order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order.IsNullOrEmpty())
        {
            return null;
        }

        await _unitOfWork.Orders.DeleteAsync(orderId);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);

    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        OrderEntity? orderEntity = await _unitOfWork.Orders.GetByIdAsync(orderId);

        var order = _mapper.Map<Order>(orderEntity);

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        IEnumerable<OrderEntity>? ordersEntity = await _unitOfWork.Orders.GetAllAsync();

        IEnumerable<Order> orders = _mapper.Map<IEnumerable<Order>>(ordersEntity);

        return orders;
    }

    public async Task<IEnumerable<OrderStatus>> GetOrderStatusAsync()
    {
        IEnumerable<OrderStatusEntity>? orderStatusEntity = await _unitOfWork.Orders.GetAllStatusAsync();

        return _mapper.Map<IEnumerable<OrderStatus>>(orderStatusEntity);
    }

    public async Task<bool?> UpdateOrderAsync(int orderId, OrderUpdateDto OrderUpdateDto)
    {
        OrderEntity? orderEntity = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (orderEntity.IsNullOrEmpty())
        {
            return null;
        }

        _mapper.Map(OrderUpdateDto, orderEntity);

        _unitOfWork.Orders.Update(orderEntity!);
        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        return rowsUpdated.IsGreaterThan(0);
    }
}
