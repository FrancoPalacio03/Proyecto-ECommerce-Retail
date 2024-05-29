using Application.Request;
using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.sale
{
    public interface ISaleService
    {
        Task<SaleResponse> CreateSale(SaleRequest saleRequest);
        Task<List<SaleGetResponse>> GetAll(DateTime? from, DateTime? to);
        Task<SaleResponse> GetSaleById(int saleId);
    }
}
