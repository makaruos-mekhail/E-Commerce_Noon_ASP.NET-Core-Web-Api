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
    public class ProductImageRepository : Repository<ProductImage, long>, IProductImageRepository
    {
        public ProductImageRepository(DContext context) : base(context)
        {
        }

        public Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            IEnumerable<ProductImage> productimages = _context.ProductImages;
            return Task.FromResult(productimages);
        }
    }
}
