using Management.Application.Common.Interfaces.Services;
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
    }
}
