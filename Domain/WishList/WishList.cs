using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WishList
{
    public class WishList
    {
      
        public long Id {get; set; }
        public long ProductId { get; set; }
        //public virtual Product product { get; set; }
        //public virtual User user { get; set; }
        public long UserId { get; set; }
        





    }
}
