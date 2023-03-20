using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Domain.Entities;

namespace Management.Infrastructure.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;

        public ProviderService(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<IEnumerable<Provider>> GetAllProvidersAsync(bool trackChanges)
        {
            var providers = await _providerRepository.GetProvidersAsync(trackChanges);

            return providers;
        }
    }
}
