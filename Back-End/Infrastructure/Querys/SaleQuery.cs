using Application.Interfaces.sale;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class SaleQuery : ISaleQuery
    {
        private readonly AppDbContext _appDbContext;

        public SaleQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Sale> GetSaleById(int SaleId)
        {
            var sale = await _appDbContext.FindAsync<Sale>(SaleId);
            return sale;
        }

        public async Task<List<Sale>> GetSaleList(DateTime? from, DateTime? to)
        {
            var query = _appDbContext.Sales.AsQueryable();



            if (from != null)
            {
                query = query.Where(s => s.Date >= from);
            }
            if (to != null)
            {
                to = to.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(s => s.Date <= to);
            }

            return await query.ToListAsync();
        }
    }
}
