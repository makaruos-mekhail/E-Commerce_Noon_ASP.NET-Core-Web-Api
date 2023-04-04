using Domain.Entities;


namespace Application.Contracts
{
	public interface IProductRepository:IRepository<Product,long>
    {
        Task<IEnumerable<Product>> FilterByAsync(string? name = null, string? nameAr = null,
            string? Category = null, string? CategoryAr = null,
            int? fromPrice = null, int? toPrice = null,
            string? brand = null, string? brandAr = null,
            string? colorName = null);


        //Task<IEnumerable<Product>> FilterByAsync(FilterDto filterDto);

        Task<Product> GetProductDetailsById(long id);
    }
}
