using Management.Application.Shared.Errors.Exceptions.Abstractions;

namespace Management.Application.Shared.Errors.Exceptions
{
    public sealed class OrderItemNotFoundException : NotFoundException
    {
        public OrderItemNotFoundException(int id)
            : base($"OrderItem with id {id} not found")
        { }
    }
}
