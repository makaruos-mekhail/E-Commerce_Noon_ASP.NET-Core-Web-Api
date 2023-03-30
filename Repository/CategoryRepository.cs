using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DContext context) : base(context)
        {

        }
        public Task<IEnumerable<Category>> FilterByAsync(string? filter = null)
        {
            IEnumerable<Category> categories = _context.Category.Include(a => a.Products)
                .Include(a => a.SubCategories)

            .Where(a => filter == null || a.Name.ToLower().Contains(filter.ToLower()));

            return Task.FromResult(categories);

        }
    }
}