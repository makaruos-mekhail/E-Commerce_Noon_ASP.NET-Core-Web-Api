using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class Order
    {
            public long OrderId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public int Discount { get; set; }
            public int UnitPrice { get; set; }
            public virtual Product Product { get; set; }
            public virtual ICollection<OrderDetails> orderDetails { get; set; }

            //public int UserId { get; set; }
            //public User User { get; set; }

    }
}
