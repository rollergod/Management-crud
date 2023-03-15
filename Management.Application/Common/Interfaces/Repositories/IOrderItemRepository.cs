using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges);
        Task<OrderItem?> GetOrderItemByIdAsync(int id, bool trackChanges);
        Task CreateOrderItemAsync(OrderItem order);
        Task UpdateOrderItemAsync(OrderItem orderForUpdate);
        Task DeleteOrderItemAsync(OrderItem orderForDelete);
    }
}
