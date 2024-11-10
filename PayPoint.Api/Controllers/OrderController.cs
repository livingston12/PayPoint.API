using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Orders;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            IEnumerable<Order> orders = await _orderService.GetOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            Order? order = await _orderService.GetOrderByIdAsync(id);
            
            if (order.IsNullOrEmpty())
            {
                return NotFound("La orden no fue encontrada.");
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateDto orderCreateDto)
        {
            Order? order = await _orderService.AddOrderAsync(orderCreateDto);

            if (order.IsNullOrEmpty())
            {
                return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
            }

            return CreatedAtAction(nameof(GetOrder), new { id = order!.OrderId }, order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool? isDeleted = await _orderService.DeleteOrderAsync(id);

            if (isDeleted.IsNullOrEmpty())
            {
                return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
            }
            else if (isDeleted == false)
            {
                return NotFound("La orden no fue encontrada.");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            var isUpdated = await _orderService.UpdateOrderAsync(id, orderUpdateDto);

            if (isUpdated.IsNullOrEmpty())
            {
                return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
            }
            else if (isUpdated == false)
            {
                return NotFound("La orden no fue encontrada.");
            }

            return Ok(); ;
        }
    }
}