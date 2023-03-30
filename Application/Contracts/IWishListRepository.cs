using Domain.Entities;

namespace Application.Contracts
{
	public interface IWishListRepository:IRepository<WishList,long>
    {
        Task<IEnumerable<WishList>> GetAllAsync(Guid? userId=null,long? productId=null);
    }
}
