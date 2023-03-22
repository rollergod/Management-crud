using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Shared.Errors.Exceptions;
using Management.Domain.Entities;
using Management.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId,bool trackChanges)
        {
            return !trackChanges ?
                await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .AsNoTracking()
                .ToListAsync() :
                await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int orderId, int id,bool trackChanges)
        {
            return !trackChanges ?
                await _context.OrderItems
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id && o.OrderId == orderId) :
                await _context.OrderItems
                .SingleOrDefaultAsync(o => o.Id == id && o.OrderId == orderId);
        }

        public async Task CreateOrderItemAsync(int orderId,OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItemForUpdate)
        {
            _context.OrderItems.Update(orderItemForUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(OrderItem orderItemForDelete)
        {
            _context.OrderItems.Remove(orderItemForDelete);
            await _context.SaveChangesAsync();
        }
    }
}
