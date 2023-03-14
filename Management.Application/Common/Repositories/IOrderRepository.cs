using Management.Domain.Entities;

namespace Management.Application.Common.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders(bool trackChanges);
    }
}
