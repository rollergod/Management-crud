using Management.Application.Shared.Dto;
using Management.Domain.Entities;
using Mapster;

namespace Management.Application.Configs
{
    public sealed class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Order, OrderDto>()
                .IgnoreNullValues(true)
                .Map(dest => dest.Items, src => src.Items);
        }
    }
}
