using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.sale
{
    public interface ISaleCommand
    {
        Task<Sale> InsertSale(Sale sale);
        Task UpdateSale(Sale sale);
        Task RemoveSale(Sale sale);
    }
}
