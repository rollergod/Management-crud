using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Application.Shared.Dto;
using Management.Application.Shared.Errors.Exceptions;
using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace Management.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto order)
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

            var orderEntity = order.Adapt<Order>();
            await _orderRepository.CreateOrderAsync(orderEntity);

            return order;
        }   

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(OrderParameters orderParams, bool trackChanges)
        {
            var orders = await _orderRepository.GetOrdersAsync(orderParams,trackChanges);

            var ordersDto = orders.Adapt<IEnumerable<OrderDto>>();

            return ordersDto;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id, bool trackChanges)
        {
            var order = await _orderRepository.GetOrderAsync(id, trackChanges);

            if (order == null)
                throw new OrderNotFoundException(id);

            var orderDto = order.Adapt<OrderDto>();
            return orderDto;
        }

        public async Task<OrderDto> UpdateOrderAsync(int id, OrderDto orderForUpdate,bool trackChanges)
        {
            var order = await _orderRepository.GetOrderAsync(id, trackChanges);

            if (order == null)
                throw new OrderNotFoundException(id);

            _mapper.Map(orderForUpdate, order);
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

        public async Task<OrderDto> GetOrderWithItemsAsync(int orderId,bool trackChanges)
        {
            var orderWithItems = await _orderRepository.GetOrderWithItemsAsync(orderId,trackChanges);

            var orderWithItemsDto = orderWithItems.Adapt<OrderDto>();

            return orderWithItemsDto;
        }
    }
}
