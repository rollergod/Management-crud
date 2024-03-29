﻿using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync(OrderParameters orderParams, bool trackChanges);
        Task<Order> GetOrderWithItemsAsync(int orderId, bool trackChanges);
        Task<Order?> GetOrderAsync(int id, bool trackChanges);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order orderForUpdate);
        Task DeleteOrderAsync(Order orderForDelete);
    }
}
