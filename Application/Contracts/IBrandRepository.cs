using Domain.Entities;

namespace Application.Contracts
{
	public interface IBrandRepository : IRepository<Brand, int>
    {
        Task<IEnumerable<Brand>> FilterByAsync(string? name = null);
    }
}
