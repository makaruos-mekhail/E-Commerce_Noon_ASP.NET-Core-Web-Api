using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Product
{
	public class ProductConfigration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
			builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
			builder.Property(p => p.Price).IsRequired();
			builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
			builder.Property(p => p.Stock).IsRequired();
			builder.Property(p => p.Discount).IsRequired().HasDefaultValue(0);
			builder.Property(p => p.Rate).IsRequired().HasDefaultValue(0);
			//builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
			//builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId);
			//builder.HasMany(p => p.Images).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
			//builder.HasMany(p => p.Reviews).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
			//builder.HasMany(p => p.Specifications).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
			
		}
	}
}
