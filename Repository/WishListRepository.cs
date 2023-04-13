using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class WishListRepository:IWishListRepository
    {
        private readonly DContext _context;
        public WishListRepository(DContext context) 
        {
            _context = context;
        }

        public async Task<bool> AddProductToWishlist(string userEmail, long ProductId)
        {
            var user =_context.Users.Single(u=>u.UserName== userEmail);
            var wishList = _context.WishList.Include(p=>p.Products).Single(w => w.UserId == user.Id);
            var product = _context.Product.Single(p =>p.Id==ProductId);
            if (product != null && wishList != null)
            {
                wishList.Products.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductFromWishlist(string userEmail, long ProductId)
        {
            var user = _context.Users.Single(u => u.UserName == userEmail);
            var wishList = _context.WishList.Include(p => p.Products).Single(w => w.UserId == user.Id);
            var product = _context.Product.Single(p => p.Id == ProductId);
            if (product != null && wishList != null)
            {
                wishList.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<IEnumerable<Product>> GetAllAsync(string userEmail)
        {
            var user = _context.Users.Single(u => u.UserName == userEmail);
            WishList wishList = _context.WishList.Include(p => p.Products).Single(w => w.UserId == user.Id);
            IEnumerable<Product>products =wishList.Products;
            return Task.FromResult(products);

        }

        //public Task<IEnumerable<WishList>> GetAllAsync(Guid? userId = null, long? productId = null)
        //{
        //    IEnumerable<WishList> wishLists = _context.WishList;
        //    return Task.FromResult(wishLists);
        //}


    }
}
