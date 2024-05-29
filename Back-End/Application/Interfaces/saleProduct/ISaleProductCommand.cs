using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.saleProduct
{
    public interface ISaleProductCommand
    {
        Task InsertSaleProduct(SaleProduct saleProduct);
        Task RemoveSaleProduct(SaleProduct saleProduct);
    }
}
