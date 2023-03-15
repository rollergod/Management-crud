using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Domain.Entities;

namespace Management.Infrastructure.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsAsync(trackChanges);

            return orderItems;
        }
    }
}
