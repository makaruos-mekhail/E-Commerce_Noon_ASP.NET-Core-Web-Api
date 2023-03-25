using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Configuration
{
    public class OrderDetailsConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(x => x.Id);
            builder.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Property(i => i.UserId).IsRequired();
            builder.Property(i => i.Price).IsRequired();
            builder.Property(i => i.Address).IsRequired();
            builder.Property(i => i.PaymentMethod).HasMaxLength(100);
        }
    }
}
