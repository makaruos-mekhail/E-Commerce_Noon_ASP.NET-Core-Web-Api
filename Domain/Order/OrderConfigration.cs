using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class OrderConfigration
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.OrderId);
            builder.Property(o => o.OrderId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(o => o.Quantity).IsRequired();
            builder.Property(o => o.Discount).IsRequired();
            builder.Property(o => o.UnitPrice).IsRequired().HasColumnType("decimal(5,2)");
            builder.Property(o => o.ProductId).IsRequired();

        }
    }
}
