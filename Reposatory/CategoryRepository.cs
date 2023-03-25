using Application.Contracts;
using Context;
using Domain.Entities;
using E_Commerce_API.Reposatories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposatory
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DContext context) : base(context)
        {

        }
        public Task<IEnumerable<Category>> FilterByAsync(string? filter = null)
        {
            IEnumerable<Category> categories = _context.Category.Include(a => a.Products)

            .Where(a => filter == null || a.Name.ToLower().Contains(filter.ToLower()));

            return Task.FromResult(categories);

        }
    }
}