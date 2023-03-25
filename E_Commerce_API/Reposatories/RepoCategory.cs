using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Reposatories
{
    public class RepoCategory : Reposatory<Category, int>
    {
        public RepoCategory(DContext _context) : base(_context)
        {
        }
        public async Task<IEnumerable<Category>> FilterByAsync(string? filter = null)
        {
            IEnumerable<Category> result;
            if (filter != null)
            {
                result = context.Category.Where(cat => cat.Name.Contains(filter));
                return result;
            }
            else
            {
                result = context.Category.ToList();
                return result;
            }

        }
        //public async Task<IEnumerable<Category>> GetCategories()
        //{
        //    return await context.Category;
        //}
    }
}
