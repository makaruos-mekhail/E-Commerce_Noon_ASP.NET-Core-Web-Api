using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Brand
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brand");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(b => b.Name).IsRequired().
                HasMaxLength(50).HasColumnName("BrandName");
            //builder.HasMany(b=>b.Products).WithMany(p=>p.Brands).Mab

            //builder.HasMany(b=>b.Categories).WithMany(c=>c.Brands).
        }
    }


}
