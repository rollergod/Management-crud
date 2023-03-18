using Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .Property(p => p.Quantity).HasPrecision(18, 3);

            modelBuilder.Entity<Provider>()
                .HasData(
                    new Provider()
                    {
                        Id = 1,
                        Name = "Perevozchik Moscow"
                    },
                    new Provider()
                    {
                        Id = 2,
                        Name = "Aligator Company"
                    },
                    new Provider()
                    {
                        Id = 3,
                        Name = "Gruzovichkoff"
                    }
            );

            modelBuilder.Entity<Order>()
            .HasData(
                new Order()
                {
                    Id = 1,
                    Number = "TestOrder",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    ProviderId = 1,
                }
            );

            modelBuilder.Entity<OrderItem>()
            .HasData(
                new OrderItem()
                {
                    Id = 1,
                    Name = "TestOrderItem",
                    Unit = "TestUnit",
                    Quantity = 1,
                    OrderId = 1
                }
            );

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
