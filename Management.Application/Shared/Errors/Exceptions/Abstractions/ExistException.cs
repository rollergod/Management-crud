
namespace Management.Application.Shared.Errors.Exceptions.Abstractions
{
    public abstract class ExistException : Exception
    {
        public ExistException(string msg) : base(msg)
        { }
    }
}
