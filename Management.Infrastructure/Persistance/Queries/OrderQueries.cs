using Management.Application.Shared.RequestFeatures;
using Management.Domain.Entities;

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
    }
}
