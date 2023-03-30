using Domain.Entities;

namespace Application.Contracts
{
	public interface IProductColorRepository: IRepository<ProductColor, long>
    {
        Task<IEnumerable<ProductColor>> FilterByAsync(string? name = null);

    }
}
