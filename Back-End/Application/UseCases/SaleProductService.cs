using Application.Interfaces.IMappers;
using Application.Interfaces.product;
using Application.Interfaces.sale;
using Application.Interfaces.saleProduct;
using Application.Mappers;
using Application.Request;
using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class SaleProductService : ISaleProductService
    {
        private readonly ISaleProductCommand _saleProductCommand;
        private readonly ISaleProductQuery _saleProductQuery;
        private readonly ISaleProductMapper _saleProductMapper;
        private readonly ISaleService _saleService;

        public SaleProductService(ISaleProductCommand command, ISaleProductQuery query, ISaleProductMapper saleProductMapper)
        {
            _saleProductCommand = command;
            _saleProductQuery = query;
            _saleProductMapper = saleProductMapper;
        }
        public async Task CreateSaleProduct(SaleProduct saleProduct)
        {
            await _saleProductCommand.InsertSaleProduct(saleProduct);
        }

        public async Task DeleteSaleProduct(SaleProduct saleProduct)
        {
            await _saleProductCommand.RemoveSaleProduct(saleProduct);
        }

        public async Task<List<SaleProduct>> GetAll()
        {
            List<SaleProduct> saleProducts = await _saleProductQuery.GetListSaleProducts();
            return saleProducts;
        }

        public async Task<SaleProduct> GetById(int saleProductId)
        {
            var saleProduct = await _saleProductQuery.GetSaleProduct(saleProductId);
            return saleProduct;
        }

        public async Task<List<SaleProductResponse>> GetSaleProductsBySaleId(int id)
        {
            List<SaleProduct> saleProducts = await _saleProductQuery.GetSaleProductsBySaleId(id);
            List<SaleProductResponse> saleProductResponses = await _saleProductMapper.GetSaleProductsResponse(saleProducts);
            return saleProductResponses;
        }



    }
}
