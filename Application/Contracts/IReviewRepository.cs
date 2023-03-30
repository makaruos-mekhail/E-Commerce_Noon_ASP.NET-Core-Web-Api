using Domain.Entities;

namespace Application.Contracts
{
	public interface IReviewRepository : IRepository<ProductReview, long>
    {
        Task<IEnumerable<ProductReview>> GitByProductIdAscyn(long id);

    }
}
