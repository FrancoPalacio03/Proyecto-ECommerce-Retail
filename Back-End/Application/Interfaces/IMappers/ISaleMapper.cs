using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IMappers
{
    public interface ISaleMapper
    {
        Task<SaleResponse> GetSaleResponse(Sale sale, int totalQuantity);
        public Task<List<SaleGetResponse>> GetSaleGetResponse(List<Sale> Sale);
    }
}
