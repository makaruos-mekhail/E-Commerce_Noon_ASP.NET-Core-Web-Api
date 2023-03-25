using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
	public class Product
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string NameAr { get; set; }

		public string Description { get; set; }

		public string DescriptionAr { get; set; }

		public decimal Price { get; set; }

		public string? Sizes { get; set; }

		public string ImagePath { get; set; }

		public byte? Rate { get; set; }

		public long ProductCategoryId { get; set; }

		public virtual Brand Brand { get; set; }

		public virtual Category Category { get; set; }

		//public virtual ICollection<ProductImage> Images { get; set; }

		public virtual ICollection<Order> Orders { get; set; }

		public long WishListId;
		public virtual WishList WishList { get; set; }

		public virtual ICollection<ProductColor> ProductColors { get; set; }

		public virtual ICollection<ProductImage> ProductImages { get; set; }

		public virtual ICollection<ProductReview> ProductReview { get; set; }
	}
}
