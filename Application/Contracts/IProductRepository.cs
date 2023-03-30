using Domain.Entities;

namespace Application.Contracts
{
	public interface IProductRepository:IRepository<Product,long>
    {
        Task<IEnumerable<Product>> FilterByAsync(string? name=null, string? Category =null, int? fromPrice=null,int? toPrice=null, string? brand=null, long? colorId=null);

        Task<Product> GetProductDetailsById(long id);
    }
}
