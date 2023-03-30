using Domain.Entities;

namespace Application.Contracts
{
	public interface IProductImageRepository:IRepository<ProductImage,long>
    {
        Task<IEnumerable<ProductImage>> GetAllAsync();
    }
}
