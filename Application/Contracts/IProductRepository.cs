using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IProductRepository:IRepository<Product,long>
    {
        Task<IEnumerable<Product>> FilterByAsync(string? name=null, string? Category =null, int? fromPrice=null,int? toPrice=null, string? brand=null, long? colorId=null);

    }
}
