using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Brand
{
     public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection <Product>Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
