using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs
{
    public class OrderItemDTO
    {
        public long productid { get; set; }
        public int Quantity { get; set; }
        public long price { get; set; }
    }
}
