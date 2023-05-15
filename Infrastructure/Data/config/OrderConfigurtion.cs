using Core.entites.OrderAggragate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.config
{
    internal class OrderConfigurtion : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(shiping => shiping.shippingAddress, shiping => shiping.WithOwner());

            builder.Property(a => a.orderStatus)
                .HasConversion(
                status => status.ToString(),
                val => (OrderStatus)Enum.Parse(typeof(OrderStatus), val)
                );

            builder.HasMany(item => item.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
