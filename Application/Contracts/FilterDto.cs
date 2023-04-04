using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class FilterDto
    {
        public string? name { set; get; }
        public string? nameAr { set; get; }
        public string? Category { set; get; }
        public string? CategoryAr { set; get; }
        public int? fromPrice { set; get; }
        public int? toPrice { set; get; }
        public string? brand { set; get; }
        public string? brandAr { set; get; }
        public string? colorName { set; get; }
    }
}
