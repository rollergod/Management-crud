using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Application.Shared.Errors.Exceptions;
using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;
using System.Data;

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
            var dateTimeParams = new OrderParameters
            {
                StartDate = DateOnly.MinValue,
                EndDate = DateOnly.MaxValue
            };

            var orders = await _orderRepository.GetOrdersAsync(dateTimeParams,trackChanges: false);

            var isOrderExist = orders.Any(o => o.ProviderId == order.ProviderId && o.Number == order.Number);

            if(isOrderExist)
                throw new OrderWithCurrentNumberAndProviderExist(order.Number,order.ProviderId);

            await _orderRepository.CreateOrderAsync(order);

            return order;
        }   

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(OrderParameters orderParams, bool trackChanges)
        {
            var orders = await _orderRepository.GetOrdersAsync(orderParams,trackChanges);

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
            var order = await _orderRepository.GetOrderAsync(id, trackChanges: false);

            if (order == null)
                throw new OrderNotFoundException(id);

            order.ProviderId = orderForUpdate.ProviderId;
            order.Number = orderForUpdate.Number;
            order.Items = orderForUpdate.Items;

            await _orderRepository.UpdateOrderAsync(order);

            return orderForUpdate;
        }

        public async Task DeleteOrderAsync(int id,bool trackChanges)
        {
            var order = await _orderRepository.GetOrderAsync(id, trackChanges);

            if (order == null)
                throw new OrderNotFoundException(id);

            await _orderRepository.DeleteOrderAsync(order);
        }

        public async Task<Order> GetOrderWithItemsAsync(int orderId,bool trackChanges)
        {
            var ordersWithItems = await _orderRepository.GetOrderWithItemsAsync(orderId,trackChanges);

            return ordersWithItems;
        }
    }
}
