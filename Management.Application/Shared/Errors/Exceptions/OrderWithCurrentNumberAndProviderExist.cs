using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Shared.Errors.Exceptions
{
    public class OrderWithCurrentNumberAndProviderExist : Exception
    {
        public OrderWithCurrentNumberAndProviderExist(string number,int providerId)
            : base($"Order with number {number} and provider id {providerId} already exist")
        { }
    }
}
