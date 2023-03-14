using Management.Application.Common.Repositories;
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

        public async Task<IEnumerable<Order>> GetOrders(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Orders
                .AsNoTracking()
                .ToListAsync() :
                await _context.Orders
                .ToListAsync();
        }


    }
}
