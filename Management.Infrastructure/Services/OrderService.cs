using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Application.Shared.Errors.Exceptions;
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
            var orders = await _orderRepository.GetOrdersAsync(trackChanges: false);

            var isOrderExist = orders.Any(o => o.ProviderId == order.ProviderId && o.Number == order.Number);

            if(isOrderExist)
                throw new OrderWithCurrentNumberAndProviderExist(order.Number,order.ProviderId);

            await _orderRepository.CreateOrderAsync(order);

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

            if (order == null)
                throw new OrderNotFoundException(id);

            return order;
        }

        public async Task<Order> UpdateOrderAsync(int id, Order orderForUpdate,bool trackChanges)
        {
            var order = await _orderRepository.GetOrderAsync(id, trackChanges);

            if (order == null)
                throw new OrderNotFoundException(id);

            await _orderRepository.UpdateOrderAsync(orderForUpdate);

            return orderForUpdate;
        }

        public async Task DeleteOrderAsync(int id,bool trackChanges)
        {
            var order = await _orderRepository.GetOrderAsync(id, trackChanges);

            if (order == null)
                throw new OrderNotFoundException(id);

            await _orderRepository.DeleteOrderAsync(order);
        }
    }
}
