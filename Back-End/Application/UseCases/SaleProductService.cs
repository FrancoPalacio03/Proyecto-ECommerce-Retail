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
        private readonly ISaleProductQuery _saleProductQuery;
        private readonly ISaleProductMapper _saleProductMapper;
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        public SaleProductService(ISaleProductQuery query, ISaleProductMapper saleProductMapper, IProductService productService)
        {
            _saleProductQuery = query;
            _saleProductMapper = saleProductMapper;
            _productService= productService;
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

        public async Task<List<SaleProduct>> ReturnSaleProducts(List<SaleProductRequest> saleProductRequests, Sale sale)
        {
            List<SaleProduct> saleProducts = new List<SaleProduct>();
            foreach (var saleProduct in saleProductRequests)
            {
                var product = await _productService.GetProductById(saleProduct.ProductId); 
                SaleProduct saleProductCreated = new SaleProduct
                {
                    Product = saleProduct.ProductId,
                    Sale = sale.SaleId,
                    Quantity = saleProduct.Quantity,
                    Price = product.Price,
                    Discount = product.Discount,
                };
                saleProducts.Add(saleProductCreated);
            }
            return await Task.FromResult(saleProducts);
        }

    }
}
