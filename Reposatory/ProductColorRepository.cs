using Application.Contracts;
using Context;
using Domain.Entities;
using E_Commerce_API.Reposatories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposatory
{
    public class ProductColorRepository : Repository<ProductColor, long>, IProductColorRepository
    {
        public ProductColorRepository(DContext context) : base(context)
        {

        }
        public Task<IEnumerable<ProductColor>> FilterByAsync(string? name = null)
        {

            IEnumerable<ProductColor> Colors = _context.ProductColors.Where(p => name == null || p.Name.ToLower().Contains(name.ToLower()));
            return Task.FromResult(Colors);
        }
    }
}
