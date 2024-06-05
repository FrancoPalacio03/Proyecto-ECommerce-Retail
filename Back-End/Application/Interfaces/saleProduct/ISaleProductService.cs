using Application.Request;
using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.saleProduct
{
    public interface ISaleProductService
    {
        Task<List<SaleProduct>> GetAll();
        Task<List<SaleProductResponse>> GetSaleProductsBySaleId(int id);
        Task<SaleProduct> GetById(int saleProductId);
        Task<List<SaleProduct>> ReturnSaleProducts(List<SaleProductRequest> saleProductRequests, Sale sale);
    }
}
