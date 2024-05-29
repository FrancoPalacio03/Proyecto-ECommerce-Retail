using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.sale
{
    public interface ISaleQuery
    {
        Task<List<Sale>> GetSaleList(DateTime? from, DateTime? to);
        Task<Sale> GetSaleById(int SaleId);
    }
}
