using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Shared.Errors.Exceptions;
using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges);
        Task<OrderItem> GetOrderItemByIdAsync(int id, bool trackChanges);
        Task<OrderItem> CreateOrderItemAsync(OrderItem orderItemForCreate);
        Task<OrderItem> UpdateOrderItemAsync(int id, OrderItem orderItemForUpdate, bool trackChanges);
        Task DeleteOrderItemAsync(int id, bool trackChanges);
        
    }
}
