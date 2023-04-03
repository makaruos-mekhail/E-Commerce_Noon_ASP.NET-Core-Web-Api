using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class ReviewRepository : Repository<ProductReview, long>, IReviewRepository
    {
        public ReviewRepository(DContext context) : base(context)
        {
        }

        public Task<IEnumerable<ProductReview>> GitByProductIdAscyn(long productid)
        {
            IEnumerable<ProductReview> productReviews = _context.ProductReviews.Include(r=>r.User)
                .Where(r => r.Product.Id == productid);

            return Task.FromResult(productReviews);

        }

        //public Task<IEnumerable<ProductReview>> FilterByAsync(long id)
        //{
        //IEnumerable<ProductReview> productReviews = _context.ProductReviews;

        //if (!string.IsNullOrWhiteSpace(Reviewfilter))
        //{
        //    productReviews = productReviews.Where(p => p.Review.Contains(Reviewfilter));
        //}

        //if (Ratefilter != null)
        //{
        //    productReviews = productReviews.Where(p => p.Rate == Ratefilter);
        //}
        //return Task.FromResult(productReviews);

        //    IEnumerable<ProductReview> productReviews = _context.ProductReviews
        //        .Where(p => Reviewfilter == null|| p.Review.Contains(Reviewfilter.ToLower()))
        //        .Where(p => Ratefilter == null || p.Rate <= Ratefilter);

        //    return Task.FromResult(productReviews);
        //}
    }
}
