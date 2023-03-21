using Management.Application.Common.Interfaces.Services;
using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderParameters orderParams)
        {
            var orders = await _orderService.GetAllOrdersAsync(orderParams,trackChanges: false);

            return Ok(orders);
        }

        [HttpGet("{orderId:int}/orderItems")]
        public async Task<IActionResult> GetOrdersWithItems(int orderId)
        {
            var ordersWithItems = await _orderService.GetOrderWithItemsAsync(orderId, trackChanges: false);

            return Ok(ordersWithItems);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id,trackChanges: false);

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);

            return Ok(createdOrder);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order orderForUpdate)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderForUpdate,trackChanges: true);

            return Ok(updatedOrder);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id,trackChanges: false);

            return NoContent();
        }
    }
}
