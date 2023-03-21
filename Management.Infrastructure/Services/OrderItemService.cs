using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Application.Shared.Errors.Exceptions;
using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;

namespace Management.Infrastructure.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderItemService(
            IOrderItemRepository orderItemRepository,
            IOrderRepository orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsAsync(trackChanges);

            return orderItems;
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id, bool trackChanges)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id, trackChanges);

            if (orderItem == null)
                throw new OrderItemNotFoundException(orderItem.Id);

            return orderItem;
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItemForCreate)
        {
            var dateTimeParams = new OrderParameters
            {
                StartDate = DateOnly.MinValue, 
                EndDate = DateOnly.MaxValue
            };
            
            var orders = await _orderRepository.GetOrdersAsync(dateTimeParams,trackChanges: false);
            var orderItems = await _orderItemRepository.GetOrderItemsAsync(trackChanges: false);

            //OrderItem.Name не может быть равен Order.Number(ограничение предметной области).
            var isOrderWithNumberExist = orders.Any(o => o.Number == orderItemForCreate.Name);

            if (isOrderWithNumberExist)
            {
                throw new OrderWithCurrentNameExist(orderItemForCreate.Name);
            }

            await _orderItemRepository.CreateOrderItemAsync(orderItemForCreate);
            return orderItemForCreate;
        }

        public async Task<OrderItem> UpdateOrderItemAsync(int id,OrderItem orderItemForUpdate,bool trackChanges)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id, trackChanges);

            if (orderItem == null)
                throw new OrderItemNotFoundException(id);

            await _orderItemRepository.UpdateOrderItemAsync(orderItemForUpdate);

            return orderItemForUpdate;
        }

        public async Task DeleteOrderItemAsync(int id,bool trackChanges)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id, trackChanges);

            if (orderItem == null)
                throw new OrderItemNotFoundException(id);

            await _orderItemRepository.DeleteOrderItemAsync(orderItem);
        }
    }
}
