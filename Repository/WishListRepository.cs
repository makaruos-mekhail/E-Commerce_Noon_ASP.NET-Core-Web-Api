using Application.Contracts;
using Context;
using Domain.Entities;

namespace Repository
{
	public class WishListRepository : Repository<WishList, long>, IWishListRepository
    {
        public WishListRepository(DContext context) : base(context)
        {
        }

        public Task<IEnumerable<WishList>> GetAllAsync(Guid? userId = null, long? productId = null)
        {
            IEnumerable<WishList> wishLists = _context.WishList;
            return Task.FromResult(wishLists);
        }

       
    }
}
