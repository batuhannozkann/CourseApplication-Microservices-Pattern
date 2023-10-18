using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.Configurations
{
    public class OrderConfiguration:IEntityTypeConfiguration<Domain.OrderAggregate.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.OrderAggregate.Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Address).WithOwner();
        }
    }
}
