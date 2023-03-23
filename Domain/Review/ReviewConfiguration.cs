using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Review
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.ProductId).IsRequired();
            builder.Property(r => r.UserReview).HasMaxLength(300);
            builder.Property(r => r.Rate).HasDefaultValue(0);
               

        }
    }
}
