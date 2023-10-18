using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Domain.OrderAggregate;
using Order.Infrastructure.Configurations;

namespace Order.Infrastructure.Contexts
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);

        }
    }
}
