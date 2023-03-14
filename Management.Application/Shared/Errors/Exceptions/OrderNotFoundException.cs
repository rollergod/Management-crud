using Management.Application.Shared.Errors.Exceptions.Abstractions;

namespace Management.Application.Shared.Errors.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(int id) 
            : base($"Order with id {id} not found")
        { }
    }
}
