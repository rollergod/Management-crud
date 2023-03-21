using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(OrderParameters orderParams,bool trackChanges);
        Task<Order> GetOrderWithItemsAsync(int orderId, bool trackChanges);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id, bool trackChanges);
        Task<Order> UpdateOrderAsync(int id, Order orderForUpdate,bool trackChanges);
        Task DeleteOrderAsync(int id, bool trackChanges);
    }
}
