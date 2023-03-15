using Management.Application.Common.Interfaces.Repositories;
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

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges)
        {
            return !trackChanges ?
                await _context.OrderItems
                .AsNoTracking()
                .ToListAsync() :
                await _context.OrderItems
                .ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.OrderItems
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id) :
                await _context.OrderItems
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task CreateOrderItemAsync(OrderItem orderItem)
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
