using Application.Exceptions;
using Application.Interfaces.IMappers;
using Application.Interfaces.product;
using Application.Interfaces.sale;
using Application.Interfaces.saleProduct;
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
    public class SaleService : ISaleService
    {
        private readonly ISaleCommand _saleCommand;
        private readonly ISaleQuery _saleQuery;
        private readonly ISaleProductService _saleProductServices;
        private readonly ISaleMapper _saleMapper;
        private readonly IProductService _productService;

        public SaleService(ISaleCommand command, ISaleQuery query, ISaleProductService saleProductServices, ISaleMapper saleMapper, IProductService productService)
        {
            _saleCommand = command;
            _saleQuery = query;
            _saleProductServices = saleProductServices;
            _saleMapper = saleMapper;
            _productService = productService;
        }
        public async Task<SaleResponse> CreateSale(SaleRequest saleRequest)
        {
            Dictionary<ProductResponse, int> productsWithQuantity = await ObtainProducts(saleRequest);
            decimal subTotal = CalculateSubTotal(productsWithQuantity);
            decimal totalDiscount = CalculateTotalDiscount(productsWithQuantity);
            decimal taxes = 1.21m; 
            decimal totalPay = CalculateTotal(subTotal, totalDiscount, taxes);
            int totalQuantity = 0;
            List<SaleProduct> saleProducts = new List<SaleProduct>();

            if (saleRequest.TotalPayed == totalPay)
            {
                foreach (KeyValuePair<ProductResponse, int> product in productsWithQuantity)
                {
                    totalQuantity += product.Value;
                }
                var sale = new Sale
                {
                    TotalPay = totalPay,
                    SubTotal = subTotal,
                    TotalDiscount = totalDiscount,
                    Taxes = taxes,
                    Date = DateTime.Now,
                    SaleProducts = saleProducts
                };

                var createdSale = await _saleCommand.InsertSale(sale);

                foreach (KeyValuePair<ProductResponse, int> product in productsWithQuantity)
                {
                    SaleProduct saleProduct = new SaleProduct
                    {
                        Product = product.Key.Id,
                        Sale = sale.SaleId,
                        Quantity = product.Value,
                        Price = product.Key.Price,
                        Discount = product.Key.Discount
                    };
                    saleProducts.Add(saleProduct);
                    await _saleProductServices.CreateSaleProduct(saleProduct);
                }

                var saleResponse = await _saleMapper.GetSaleResponse(createdSale, totalQuantity);

                return saleResponse;
            }
            else
            {
                throw new Exception("Total Pay calculate error");
            }
        }

        public async Task<List<SaleGetResponse>> GetAll(DateTime? from, DateTime? to)
        {
            List<Sale> sale = await _saleQuery.GetSaleList(from, to);
            return await _saleMapper.GetSaleGetResponse(sale);
        }

        public async Task<SaleResponse> GetSaleById(int saleId)
        {
            try
            {
                if (!await CheckSaleId(saleId))
                {
                    throw new ExceptionNotFound("No Existe ese ID");
                }
                var sale = await _saleQuery.GetSaleById(saleId);
                List<SaleProductResponse> saleProductResponses = await _saleProductServices.GetSaleProductsBySaleId(saleId);
                int totalQuantity = 0;
                foreach (SaleProductResponse saleProductResponse in saleProductResponses)
                {
                    totalQuantity += saleProductResponse.Quantity;
                }
                return await _saleMapper.GetSaleResponse(sale, totalQuantity);
            } catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound(ex.Message);
            }
        }


        private async Task<bool> CheckSaleId(int id)
        {
            return (await _saleQuery.GetSaleById(id) != null);
        }

        private decimal CalculateTotal(decimal subTotal, decimal totalDiscount, decimal taxes)
        {
            decimal total = (subTotal - totalDiscount) * taxes;
            return total;
        }

        private decimal CalculateSubTotal(Dictionary<ProductResponse, int> productsAndQuantities)
        {
            decimal total = 0m;
            foreach (KeyValuePair<ProductResponse, int> product in productsAndQuantities)
            {
                total += product.Key.Price * product.Value;
            };
            return total;
        }

        private decimal CalculateTotalDiscount(Dictionary<ProductResponse, int> productsAndQuantities)
        {
            decimal totalDiscount = 0;
            foreach (KeyValuePair<ProductResponse, int> product in productsAndQuantities)
            {
                if (product.Key.Discount > 0)
                {
                    decimal percentage = product.Key.Discount / 100m;
                    decimal unitDiscount = product.Key.Price * percentage;
                    totalDiscount += product.Value * unitDiscount;
                }
            };
            return totalDiscount;
        }

        public async Task<Dictionary<ProductResponse, int>> ObtainProducts(SaleRequest request)
        {
            Dictionary<ProductResponse, int> productsAndQuantities = new Dictionary<ProductResponse, int>();
            foreach (SaleProductRequest saleProduct in request.Products)
            {
                ProductResponse currentProduct = await _productService.GetProductById(saleProduct.ProductId);
                productsAndQuantities.Add(currentProduct, saleProduct.Quantity);
            };
            return productsAndQuantities;
        }


    }
}
