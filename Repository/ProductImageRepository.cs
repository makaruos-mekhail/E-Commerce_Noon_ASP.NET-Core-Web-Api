using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class ProductImageRepository : Repository<ProductImage, long>, IProductImageRepository
    {
        public ProductImageRepository(DContext context) : base(context)
        {
        }

        public Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            IEnumerable<ProductImage> productimages = _context.ProductImages.Include(a => a.Product);
            return Task.FromResult(productimages);
        }
    }
}
