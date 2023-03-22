using Management.Application.Common.Interfaces.Services;
using Management.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId}/orderitem")] 
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
            var orderItems = await _orderItemService.GetOrderItemsAsync(orderId,trackChanges: false);

            return Ok(orderItems);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GerOrderItemById(int orderId, int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(orderId,id, trackChanges: false);

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int orderId, OrderItem orderItem)
        {
            var createdOrderItem = await _orderItemService.CreateOrderItemAsync(
                orderId,
                orderItem,
                trackChanges: false);

            return Ok(createdOrderItem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrderItem(int orderId, int id, [FromBody] OrderItem orderItem)
        {
            var updatedOrder = await _orderItemService.UpdateOrderItemAsync(
                orderId,
                id,
                orderItem,
                trackChanges: false);

            return Ok(updatedOrder);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrderAsync(int orderId, int id)
        {
            await _orderItemService.DeleteOrderItemAsync(orderId, id, trackChanges: false);

            return NoContent();
        }
    }
}
