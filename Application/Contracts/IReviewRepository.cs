using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IReviewRepository : IRepository<ProductReview, long>
    {
        Task<IEnumerable<ProductReview>> GitByProductIdAscyn(long id);

    }
}
