using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges);
    }
}
