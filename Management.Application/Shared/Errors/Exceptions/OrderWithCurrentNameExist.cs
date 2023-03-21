using Management.Application.Shared.Errors.Exceptions.Abstractions;
using Management.Domain.Entities;

namespace Management.Application.Shared.Errors.Exceptions
{
    public class OrderWithCurrentNameExist : ExistException
    {
        public OrderWithCurrentNameExist(string name) 
            : base($"The {nameof(Order)} can`t be created with current number - {name} because the OrderItem already has it.")
        {
        }
    }
}
