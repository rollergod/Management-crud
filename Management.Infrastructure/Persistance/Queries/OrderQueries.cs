using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Management.Infrastructure.Persistance.Queries
{
    public static class OrderQueries
    {
        public static IQueryable<Order> DateQuery(this IQueryable<Order> orders, OrderParameters orderParams)
        {
            // StartDate = 21/02/2023
            // EndDate = 21/03/2023
            // Date = 15/03/2023
            return orders
                .Where(o => o.Date >= orderParams.StartDate 
                    && o.Date <= orderParams.EndDate);
        }

        public static IQueryable<Order> Sort(this IQueryable<Order> orders,string orderByQuery)
        {
            if (string.IsNullOrEmpty(orderByQuery))
                return orders.OrderBy(o => o.Id);

            var orderParams = orderByQuery.Trim().Split(',');
            var propertyInfos = typeof(Order).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach(var param in orderParams)
            {
                if (string.IsNullOrEmpty(param))
                    continue;

                var propNameFromQuery = param.Split(" ").First();
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propNameFromQuery, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrEmpty(orderQuery))
                return orders.OrderBy(o => o.Id);

            return orders.OrderBy(orderByQuery);
        }
    }
}
