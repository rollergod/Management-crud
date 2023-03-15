using Management.Application.Common.Interfaces.Services;
using Management.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems()
        {
            var orderItems = await _orderItemService.GetOrderItemsAsync(trackChanges: false);

            return Ok(orderItems);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GerOrderItemById(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id, trackChanges: false);

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderItem orderItem)
        {
            var createdOrderItem = await _orderItemService.CreateOrderItemAsync(orderItem);

            return Ok(createdOrderItem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            var updatedOrder = await _orderItemService.UpdateOrderItemAsync(
                id,
                orderItem,
                trackChanges: false);

            return Ok(updatedOrder);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            await _orderItemService.DeleteOrderItemAsync(id, trackChanges: false);

            return NoContent();
        }
    }
}
