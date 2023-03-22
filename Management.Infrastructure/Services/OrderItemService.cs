using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Application.Shared.Dto;
using Management.Application.Shared.Errors.Exceptions;
using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;
using Mapster;

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

        public async Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync(int orderId,bool trackChanges)
        {
            await CheckIfOrderExist(orderId,trackChanges);

            var orderItems = await _orderItemRepository.GetOrderItemsAsync(orderId,trackChanges);

            var orderItemsDto = orderItems.Adapt<IEnumerable<OrderItemDto>>();
            return orderItemsDto;
        }

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int orderId,int id, bool trackChanges)
        {
            await CheckIfOrderExist(orderId,trackChanges);

            var orderItem = await GetOrderItemAndCheckIfItExist(orderId, id, trackChanges);

            var orderItemDto = orderItem.Adapt<OrderItemDto>();

            return orderItemDto;
        }

        public async Task<OrderItemDto> CreateOrderItemAsync(int orderId,OrderItem orderItemForCreate, bool trackChanges)
        {
            var dateTimeParams = new OrderParameters
            {
                StartDate = DateOnly.MinValue, 
                EndDate = DateOnly.MaxValue
            };
            
            var orders = await _orderRepository.GetOrdersAsync(dateTimeParams,trackChanges);

            //OrderItem.Name не может быть равен Order.Number(ограничение предметной области).
            var isOrderWithNumberExist = orders.Any(o => o.Number == orderItemForCreate.Name);

            if (isOrderWithNumberExist)
                throw new OrderWithCurrentNameExist(orderItemForCreate.Name);

            await _orderItemRepository.CreateOrderItemAsync(orderId, orderItemForCreate);

            var orderItemDto = orderItemForCreate.Adapt<OrderItemDto>();
            return orderItemDto;
        }

        public async Task<OrderItemDto> UpdateOrderItemAsync(
            int orderId,
            int id,
            OrderItem orderItemForUpdate,
            bool trackChanges)
        {
            await CheckIfOrderExist(orderId, trackChanges);

            await GetOrderItemAndCheckIfItExist(orderId, id, trackChanges);

            await _orderItemRepository.UpdateOrderItemAsync(orderItemForUpdate);

            var orderItemDto = orderItemForUpdate.Adapt<OrderItemDto>();
            return orderItemDto;
        }

        public async Task DeleteOrderItemAsync(int orderId,int id,bool trackChanges)
        {
            await CheckIfOrderExist(orderId, trackChanges);

            var orderItem = await GetOrderItemAndCheckIfItExist(orderId,id,trackChanges);

            await _orderItemRepository.DeleteOrderItemAsync(orderItem);
        }

        private async Task CheckIfOrderExist(int orderId,bool trackChanges)
        {
            var order = await _orderRepository.GetOrderAsync(orderId,trackChanges);

            if (order == null)
                throw new OrderNotFoundException(orderId);
        }

        private async Task<OrderItem> GetOrderItemAndCheckIfItExist(int orderId,int id,bool trackChanges)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(orderId, id, trackChanges);

            if (orderItem == null)
                throw new OrderItemNotFoundException(id);

            return orderItem;
        }
    }
}
