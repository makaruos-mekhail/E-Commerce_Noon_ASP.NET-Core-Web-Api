using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Product
{
	public class Product
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public int BrandId { get; set; }
		public virtual Brand Brand { get; set; }
		public int Stock { get; set; }
		public byte Discount { get; set; }
		public byte Rate { get; set; }
		public virtual ICollection<ProductImage> Images { get; set; }
		public virtual ICollection<Review> Reviews { get; set; }
		public virtual ICollection<Specification> Specifications { get; set; }

	}
}
