using Management.Application.Common.Interfaces.Repositories;
using Management.Domain.Entities;
using Management.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly AppDbContext _context;

        public ProviderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provider>> GetProvidersAsync(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Providers
                .AsNoTracking()
                .ToListAsync()
                : await _context.Providers
                .ToListAsync();
        }
    }
}
