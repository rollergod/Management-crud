using Management.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            var providers = await _providerService.GetAllProvidersAsync(trackChanges: false);

            return Ok(providers);
        }
    }
}
