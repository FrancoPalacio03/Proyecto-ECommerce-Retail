using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IMappers
{
    public interface ISaleProductMapper
    {
        Task<SaleProductResponse> GetSaleProductResponse(SaleProduct saleProduct);
        Task<List<SaleProductResponse>> GetSaleProductsResponseDictionary(Sale sale, Dictionary<ProductResponse, int> productsWithQuantities);
        Task<List<SaleProductResponse>> GetSaleProductsResponse(List<SaleProduct> saleProducts);
    }
}
