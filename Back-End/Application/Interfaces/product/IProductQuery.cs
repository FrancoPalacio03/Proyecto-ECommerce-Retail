using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.product
{
    public interface IProductQuery
    {
        Task<List<Product>> GetListProducts(string? name, int? limit, int? offset, int[]? categories);
        Task<Product> GetProduct(Guid productId);
    }
}
