using Management.Domain.Entities;

namespace Management.Application.Common.Interfaces.Services
{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetAllProvidersAsync(bool trackChanges);
    }
}
