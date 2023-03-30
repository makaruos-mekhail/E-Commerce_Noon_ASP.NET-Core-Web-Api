using Application.Contracts;
using Context;
using Domain.Entities;

namespace Repository
{
	public class ProductColorRepository : Repository<ProductColor, long>, IProductColorRepository
    {
        public ProductColorRepository(DContext context) : base(context)
        {

        }
        public Task<IEnumerable<ProductColor>> FilterByAsync(string? name = null)
        {

            IEnumerable<ProductColor> Colors = _context.ProductColors
                .Where(p => name == null || p.Name.ToLower().Contains(name.ToLower()));
            return Task.FromResult(Colors);
        }
    }
}
