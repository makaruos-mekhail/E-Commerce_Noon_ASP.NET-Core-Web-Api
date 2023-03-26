using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IWishListRepository:IRepository<WishList,long>
    {
        Task<IEnumerable<WishList>> GetAllAsync(Guid? userId=null,long? productId=null);
    }
}
