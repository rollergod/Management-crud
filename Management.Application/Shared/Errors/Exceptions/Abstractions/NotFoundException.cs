namespace Management.Application.Shared.Errors.Exceptions.Abstractions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message) 
            : base(message)
        { }
    }
}
