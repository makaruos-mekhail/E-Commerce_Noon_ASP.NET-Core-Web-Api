using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository
{
	public class ProductRepository : Repository<Product, long>, IProductRepository
    {
        public ProductRepository(DContext context) : base(context)
        {
            
        }

        public Task<IEnumerable<Product>> FilterByAsync(string? name = null, string? Category = null, int? fromPrice = null, int? toPrice = null, string? brand = null, long? colorId = null)
        {
            IEnumerable<Product> products = _context.Product.Include(a => a.ProductImages)
                .Where(p => name == null || p.Name.ToLower().Contains(name.ToLower()))
                .Where(p => Category == null || p.Category.Name == Category)
                .Where(p => fromPrice == null || p.Price >= fromPrice)
                .Where(p => toPrice == null || p.Price <= toPrice)
                .Where(p => brand == null || p.Brand.Name == brand)
                .Where(p => colorId == null || p.ProductColors.Any(p => p.Id == colorId));   

            return Task.FromResult( products);
        }

        public Task<Product> GetProductDetailsById(long id)
        {
            var product = _context.Product.Include(p => p.ProductImages)
                 .Include(p => p.ProductColors).Include(p => p.ProductReview)
                 .Include(p => p.Brand).Single(p => p.Id == id);

            return Task.FromResult(product);
        }
    }
}
