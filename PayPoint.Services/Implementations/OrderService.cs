using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Order?> AddOrderAsync(OrderCreateDto orderCreateDto)
    {
        bool hasOrderDetails = orderCreateDto.OrderDetails?.Any() == true;

        if (hasOrderDetails)
        {
            await _unitOfWork.BeginTransactionAsync();
        }

        try
        {
            OrderEntity orderEntity = _mapper.Map<OrderEntity>(orderCreateDto);

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
                await AddOrderDetailsToOrder(orderEntity, orderCreateDto.OrderDetails!.ToList());

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }

            return _mapper.Map<Order>(orderEntity);
        }
        catch (Exception)
        {
            if (hasOrderDetails)
            {
                await _unitOfWork.RollbackTransactionAsync();
            }
            return null;
        }
    }

    public async Task<OrderDetail?> AddUpdateOrderDetailAsync(int orderId, IEnumerable<OrderDetailDto> orderDetails)
    {
        OrderEntity? orderEntity = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (orderEntity.IsNullOrEmpty())
        {
            return null;
        }

        await UpdateOrderDetailsToOrder(orderEntity!, orderDetails);
        await _unitOfWork.SaveChangesAsync();

        // Retrieve the updated order.
        OrderEntity? updatedOrderEntity = await _unitOfWork.Orders
                                                    .AsQueryable()
                                                    .Include(x => x.OrderDetails)
                                                    .FirstOrDefaultAsync(x => x.OrderId == orderId);

        return new OrderDetail()
        {
            OrderId = updatedOrderEntity!.OrderId,
            Details = _mapper.Map<IEnumerable<OrderDetailDto>>(updatedOrderEntity!.OrderDetails)
        };
    }
    public async Task<bool?> UpdateOrderAsync(int orderId, OrderUpdateDto orderUpdateDto)
    {
        bool hasOrderDetails = orderUpdateDto.OrderDetails?.Any() == true;

        if (hasOrderDetails)
        {
            await _unitOfWork.BeginTransactionAsync();
        }

        try
        {
            OrderEntity? orderEntity = await _unitOfWork.Orders.GetByIdAsync(orderId);

            if (orderEntity.IsNullOrEmpty())
            {
                return null;
            }

            _mapper.Map(orderUpdateDto, orderEntity);

            _unitOfWork.Orders.Update(orderEntity!);
            int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

            if (hasOrderDetails)
            {
                await UpdateOrderDetailsToOrder(orderEntity!, orderUpdateDto.OrderDetails!);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }

            return rowsUpdated.IsGreaterThan(0);
        }
        catch (Exception)
        {
            if (hasOrderDetails)
            {
                await _unitOfWork.RollbackTransactionAsync();
            }
            return null;
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

        return _mapper.Map<Order>(orderEntity);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        IEnumerable<OrderEntity>? ordersEntity = await _unitOfWork.Orders.GetAllAsync();

        IEnumerable<Order> orders = _mapper.Map<IEnumerable<Order>>(ordersEntity);

        return orders;
    }

    public async Task<IEnumerable<OrderStatus>> GetOrderStatusAsync()
    {
        IEnumerable<OrderStatusEntity>? orderStatusEntity = await _unitOfWork.OrderStatus.GetAllAsync();

        return _mapper.Map<IEnumerable<OrderStatus>>(orderStatusEntity);
    }

    public async Task<OrderStatus?> GetOrderStatusByIdAsync(int statusId)
    {
        OrderStatusEntity? orderStatusEntity = await _unitOfWork.OrderStatus.GetByIdAsync(statusId);

        return _mapper.Map<OrderStatus>(orderStatusEntity);
    }

    public async Task<OrderStatus?> AddOrderStatusAsync(OrderStatusCreateDto orderStatusCreateDto)
    {
        OrderStatusEntity orderStatusEntity = _mapper.Map<OrderStatusEntity>(orderStatusCreateDto);

        await _unitOfWork.OrderStatus.AddAsync(orderStatusEntity);
        int? rowInserted = await _unitOfWork.SaveChangesAsync();

        if (rowInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<OrderStatus>(orderStatusEntity);
    }

    public async Task<bool?> UpdateOrderStatusAsync(int statusId, OrderStatusCreateDto orderStatusUpdateDto)
    {
        OrderStatusEntity? orderStatusEntity = await _unitOfWork.OrderStatus.GetByIdAsync(statusId);

        if (orderStatusEntity.IsNullOrEmpty())
        {
            return null;
        }

        _mapper.Map(orderStatusUpdateDto, orderStatusEntity);
        _unitOfWork.OrderStatus.Update(orderStatusEntity!);
        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        return rowsUpdated.IsGreaterThan(0);
    }

    public async Task<bool?> DeleteOrderStatusAsync(int statusId)
    {
        OrderStatusEntity? orderStatusEntity = await _unitOfWork.OrderStatus.GetByIdAsync(statusId);

        if (orderStatusEntity.IsNullOrEmpty())
        {
            return null;
        }

        await _unitOfWork.OrderStatus.DeleteAsync(statusId);
        int? rowDeleted = await _unitOfWork.SaveChangesAsync();

        return rowDeleted.IsGreaterThan(0);
    }

    private async Task UpdateOrderDetailsToOrder(OrderEntity orderEntity, IEnumerable<OrderDetailDto> orderDetails)
    {
        IEnumerable<int>? updateProductIds = orderDetails.Select(x => x.ProductId);

        // Retrieve existing order details matching the criteria.
        IQueryable<OrderDetailEntity> queryOrderDetails = _unitOfWork.Orders.DetailAsQueryable();
        List<OrderDetailEntity>? existingOrderDetails = queryOrderDetails
        .Where(x => x.OrderId == orderEntity.OrderId && updateProductIds.Contains(x.ProductId))
        .ToList();

        existingOrderDetails.ForEach(_unitOfWork.Orders.UpdateDetail);

        // Identify the new product IDs for insertion (those not already existing).
        List<int> existingProductIds = existingOrderDetails.Select(detail => detail.ProductId).ToList();
        List<int> newProductIds = updateProductIds.Except(existingProductIds).ToList();

        // Prepare new order details for insertion.
        List<OrderDetailDto> newOrderDetails = newProductIds
        .Select(productId => new OrderDetailDto
        {
            ProductId = productId,
            Quantity = 1,
        })
        .ToList();

        // Insert the new order details.
        if (newOrderDetails.Any())
        {
            await AddOrderDetailsToOrder(orderEntity, newOrderDetails);
        }
    }

    private async Task AddOrderDetailsToOrder(OrderEntity orderEntity, List<OrderDetailDto> orderDetails)
    {
        orderEntity.OrderDetails = new List<OrderDetailEntity>();

        foreach (OrderDetailDto orderDetail in orderDetails)
        {
            ProductEntity? productExist = await _unitOfWork.Products.GetByIdAsync(orderDetail.ProductId);

            // If the product does not exist, skip it.
            if (productExist.IsNullOrEmpty())
            {
                continue;
            }

            OrderDetailEntity orderDetailEntity = _mapper.Map<OrderDetailEntity>(orderDetail);
            orderDetailEntity.OrderId = orderEntity.OrderId;
            orderDetailEntity.UnitPrice = productExist!.Price;

            await _unitOfWork.Orders.AddDetailAsync(orderDetailEntity);
        }
    }

}
