using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Repositories
{
    public interface IProviderRepository
    {
        Task<IEnumerable<Provider>> GetProvidersAsync(bool trackChanges);
    }
}
