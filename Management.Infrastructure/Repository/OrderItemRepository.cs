using Management.Application.Common.Interfaces.Repositories;
using Management.Domain.Entities;
using Management.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;
        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(bool trackChanges)
        {
            return !trackChanges ?
                await _context.OrderItems
                .AsNoTracking()
                .ToListAsync() :
                await _context.OrderItems
                .ToListAsync();
        }
    }
}
