using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IMappers
{
    public interface IProductMapper
    {
        public Task<ProductResponse> GetProductResponse(Product product);
        public Task<List<ProductGetResponse>> GetProductGetResponse(List<Product> products);
    }
}
