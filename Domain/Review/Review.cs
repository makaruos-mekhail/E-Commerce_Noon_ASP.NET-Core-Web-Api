using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Review
{
    public class Review
    {
        public long Id { get; set; }
        public string UserReview { get; set; }
        public byte Rate { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
