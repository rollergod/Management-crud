using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;
using Management.Infrastructure.Persistance;
using Management.Infrastructure.Persistance.Queries;
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

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderParameters orderParams,bool trackChanges)
        {
            return !trackChanges ?
                await _context.Orders
                .AsNoTracking()
                .DateQuery(orderParams)
                .Sort(orderParams.OrderBy)
                .ToListAsync() :
                await _context.Orders
                .DateQuery(orderParams)
                .Sort(orderParams.OrderBy)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderAsync(int id,bool trackChanges)
        {
            return !trackChanges ?
                await _context.Orders
                .Include(o => o.Items)
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
            var validItems = orderForUpdate.Items.Select(i => i.Id).ToList();

            var missingItems = await _context.OrderItems
                .Where(oi => oi.OrderId == orderForUpdate.Id 
                    && !validItems.Contains(oi.Id))
                .ToListAsync();

            if (missingItems.Count > 0)
            { 
                _context.OrderItems.RemoveRange(missingItems);
            }

            _context.Orders.Update(orderForUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order orderForDelete)
        {
            _context.Orders.Remove(orderForDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderWithItemsAsync(int orderId,bool trackChanges)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            _context.Entry(order)
                .Collection(o => o.Items).Load();
            return order;
        }
    }
}
