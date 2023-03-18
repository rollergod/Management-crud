using Management.Application.Common.Interfaces.Repositories;
using Management.Domain.Entities;
using Management.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Orders
                .Include(o => o.Items)
                .AsNoTracking()
                .ToListAsync() :
                await _context.Orders
                .ToListAsync();
        }

        public async Task<Order?> GetOrderAsync(int id,bool trackChanges)
        {
            return !trackChanges ?
                await _context.Orders
                .AsNoTracking()
                .SingleOrDefaultAsync(order => order.Id == id) :
                await _context.Orders
                .SingleOrDefaultAsync(order => order.Id == id);
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order orderForUpdate)
        {
            _context.Orders.Update(orderForUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order orderForDelete)
        {
            _context.Orders.Remove(orderForDelete);
            await _context.SaveChangesAsync();
        }
    }
}
