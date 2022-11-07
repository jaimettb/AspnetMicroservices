using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infraestructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContext}", typeof(OrderContext));
            }
        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>()
            {
                new Order(){UserName = "swn", FirstName = "Jaime", LastName = "Batista", EmailAddress = "jaimettb@gmail.com", AddressLine = "Address 1", Country = "Brasil", TotalPrice = 350 }
            };
        }
    }
}
