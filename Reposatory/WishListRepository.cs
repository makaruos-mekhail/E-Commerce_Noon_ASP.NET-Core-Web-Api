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
