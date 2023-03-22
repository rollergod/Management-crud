using Management.Application.Shared.Dto;
using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync(int orderId, bool trackChanges);
        Task<OrderItemDto> GetOrderItemByIdAsync(int orderId, int id, bool trackChanges);
        Task<OrderItemDto> CreateOrderItemAsync(int orderId, OrderItem orderItemForCreate,bool trackChanges);
        Task<OrderItemDto> UpdateOrderItemAsync(
            int orderId,
            int id,
            OrderItem orderItemForUpdate,
            bool trackChanges);
        Task DeleteOrderItemAsync(int orderId,int id, bool trackChanges);
        
    }
}
