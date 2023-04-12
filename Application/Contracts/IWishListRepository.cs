using Domain.Entities;

namespace Application.Contracts
{
    public interface IWishListRepository//:IRepository<WishList,long>
    {
       // Task<IEnumerable<WishList>> GetAllAsync(Guid? userId=null,long? productId=null);
        Task<IEnumerable<Product>> GetAllAsync(string userEmail);
        Task<bool> AddProductToWishlist(string userEmail, long ProductId);
        Task<bool> DeleteProductFromWishlist(string userEmail , long ProductId);
    }
}
