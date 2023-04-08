using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.DTOs;

namespace Repository
{
    public class ProductRepository : Repository<Product, long>,IProductRepository
    {
        public ProductRepository(DContext context) : base(context)
        {

        }

       
        public Task<Product> GetProductDetailsById(long id)
        {
            var product = _context.Product.Include(p => p.ProductImages)
                  .Include(p => p.ProductColors).Include(p => p.ProductReview)
                .Include(p => p.Brand).Single(p => p.Id == id);

              return Task.FromResult(product);
        }
        public Task<IEnumerable<Product>> FilterByAsync(string? name = null, string? nameAr = null,
            string? Category = null, string? CategoryAr = null,
            int? fromPrice = null, int? toPrice = null,
            string? brand = null, string? brandAr = null,
             string? colorName = null)
        {
            IEnumerable<Product> products = _context.Product.Include(a => a.ProductImages)
                .Where(p => name == null || p.Name.ToLower().Contains(name.ToLower()))
                .Where(p => nameAr == null || p.NameAr.Contains(nameAr))
                .Where(p => Category == null || p.Category.Name == Category)
                .Where(p => CategoryAr == null || p.Category.NameAr == CategoryAr)
                .Where(p => fromPrice == null || p.Price >= fromPrice)
                .Where(p => toPrice == null || p.Price <= toPrice)
                .Where(p => brand == null || p.Brand.Name == brand)
                .Where(p => brandAr == null || p.Brand.NameAr == brandAr)
                .Where(p => colorName == null || p.ProductColors.Any(p => p.Name == colorName));

            return Task.FromResult(products);
        }
        public Task<List<Product>> GetProductsbyIdes(long[] ides)
        {
            List<Product> products = new List<Product>();
            foreach (long id in ides)
            {
                var product = _context.Product
                  // .Include(p => p.ProductImages)
                  // .Include(p => p.ProductColors)
                  //  .Include(p => p.ProductReview)
                  .Include(p => p.Brand).AsNoTracking().Single(p => p.Id == id);
                // .Include(p => p.Brand).Single(p => p.Id == id).Select(pp=>pp.Brand.Name);
                products.Add(product);

            }
            return Task.FromResult(products);
        }
        //public Task<IEnumerable<Product>> FilterByAsync(FilterDto filterDto)
        //{
        //    IEnumerable<Product> products = _context.Product.Include(a => a.Brand)
        //        .Include(c => c.Category).Include(c => c.ProductColors).AsNoTracking()
        //      .Where(p => filterDto.name == null || p.Name.ToLower().Contains(filterDto.name.ToLower()))
        //     .Where(p => filterDto.nameAr == null || p.NameAr.Contains(filterDto.nameAr))
        //     .Where(p => filterDto.Category == null || p.Category.Name == filterDto.Category)
        //       .Where(p => filterDto.CategoryAr == null || p.Category.NameAr == filterDto.CategoryAr)
        //        .Where(p => filterDto.fromPrice == null || p.Price >= filterDto.fromPrice)
        //        .Where(p => filterDto.toPrice == null || p.Price <= filterDto.toPrice)
        //        .Where(p => filterDto.brand == null || p.Brand.Name == filterDto.brand)
        //        .Where(p => filterDto.brandAr == null || p.Brand.NameAr == filterDto.brandAr)
        //        .Where(p => filterDto.colorName == null || p.ProductColors.Any(p => p.Name == filterDto.colorName));

        //    return Task.FromResult(products);
        //}

    }
}
