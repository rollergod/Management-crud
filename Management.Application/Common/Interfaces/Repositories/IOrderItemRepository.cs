using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId, bool trackChanges);
        Task<OrderItem?> GetOrderItemByIdAsync(int orderId, int id, bool trackChanges);
        Task CreateOrderItemAsync(int orderId, OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderForUpdate);
        Task DeleteOrderItemAsync(OrderItem orderForDelete);
    }
}
