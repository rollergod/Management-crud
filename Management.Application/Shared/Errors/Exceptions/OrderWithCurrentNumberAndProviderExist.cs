namespace Management.Application.Shared.Errors.Exceptions
{
    public sealed class OrderWithCurrentNumberAndProviderExist : Exception
    {
        public OrderWithCurrentNumberAndProviderExist(string number,int providerId)
            : base($"Order with number {number} and provider id {providerId} already exist")
        { }
    }
}
