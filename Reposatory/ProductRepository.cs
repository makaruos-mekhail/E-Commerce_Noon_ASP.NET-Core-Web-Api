using Application.Contracts;
using Context;
using Domain.Entities;
using E_Commerce_API.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposatory
{
    public class ProductRepository : Repository<Product, long>, IProductRepository
    {
        public ProductRepository(DContext context) : base(context)
        {
            
        }

        public Task<IEnumerable<Product>> FilterByAsync(string? name = null, string? Category = null, int? fromPrice = null, int? toPrice = null, string? brand = null, long? colorId = null)
        {
            IEnumerable<Product> products = _context.Product.Where(p => name == null || p.Name.ToLower().Contains(name.ToLower()))
                .Where(p => Category == null || p.Category.Name == Category)
                .Where(p => fromPrice == null || p.Price >= fromPrice)
                .Where(p => toPrice == null || p.Price <= toPrice)
                .Where(p => brand == null || p.Brand.Name == brand)
                .Where(p => colorId == null || p.ProductColors.Any(p => p.Id == colorId));   

            return Task.FromResult( products);
        }
    }
}
