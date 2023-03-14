﻿using Management.Application.Common.Interfaces.Services;
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
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync(trackChanges: false);

            return Ok(orders);
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
            var createdCompany = await _orderService.CreateOrderAsync(order);

            return Ok(createdCompany);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order orderForUpdate)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderForUpdate,trackChanges: false);

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