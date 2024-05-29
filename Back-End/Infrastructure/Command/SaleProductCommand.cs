using Application.Interfaces.saleProduct;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class SaleProductCommand : ISaleProductCommand
    {
        private readonly AppDbContext _context;

        public SaleProductCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertSaleProduct(SaleProduct saleProduct)
        {
            _context.Add(saleProduct);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSaleProduct(SaleProduct saleProduct)
        {
            _context.Remove(saleProduct);
            await _context.SaveChangesAsync();
        }
    }
}
