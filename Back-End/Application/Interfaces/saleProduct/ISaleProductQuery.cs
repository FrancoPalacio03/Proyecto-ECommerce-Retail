using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.saleProduct
{
    public interface ISaleProductQuery
    {
        Task<List<SaleProduct>> GetListSaleProducts();
        Task<SaleProduct> GetSaleProduct(int getSaleProductId);
        Task<List<SaleProduct>> GetSaleProductsBySaleId(int id);
    }
}
