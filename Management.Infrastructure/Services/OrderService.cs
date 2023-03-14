using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Domain.Entities;

namespace Management.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orderRepository.CreateOrder(order);

            return order;
        }   

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges)
        {
            var orders = await _orderRepository.GetOrdersAsync(trackChanges);

            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int id, bool trackChanges)
        {
            var order = await _orderRepository.GetOrderAsync(id, trackChanges);

            return order;
        }
    }
}
