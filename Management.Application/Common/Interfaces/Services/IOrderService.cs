using Management.Application.Shared.Dto;
using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(OrderParameters orderParams,bool trackChanges);
        Task<OrderDto> GetOrderWithItemsAsync(int orderId, bool trackChanges);
        Task<OrderDto> CreateOrderAsync(OrderDto order);
        Task<OrderDto> GetOrderByIdAsync(int id, bool trackChanges);
        Task<OrderDto> UpdateOrderAsync(int id, OrderDto orderForUpdate,bool trackChanges);
        Task DeleteOrderAsync(int id, bool trackChanges);
    }
}
