﻿using Application.Interfaces.saleProduct;
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
    public class SaleProductQuery : ISaleProductQuery
    {
        private readonly AppDbContext _appDbContext;

        public SaleProductQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<SaleProduct>> GetListSaleProducts()
        {
            return await _appDbContext.SaleProducts.ToListAsync();
        }

        public async Task<SaleProduct> GetSaleProduct(int getSaleProductId)
        {
            var saleProduct = await _appDbContext.FindAsync<SaleProduct>(getSaleProductId);
            return saleProduct;
        }
        public async Task<List<SaleProduct>> GetSaleProductsBySaleId(int id)
        {
            IQueryable<SaleProduct> saleProducts = _appDbContext.SaleProducts;
            saleProducts = saleProducts.Where(s => s.Sale == id);
            return await saleProducts.ToListAsync();
        }
    }
}
