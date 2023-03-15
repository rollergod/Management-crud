using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges);
    }
}
