using Application.Exceptions;
using Application.Interfaces.IMappers;
using Application.Interfaces.product;
using Application.Mappers;
using Application.Request;
using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ProductService : IProductService
    {
        private readonly IProductCommand _productCommand;
        private readonly IProductQuery _productQuery;
        private readonly IProductMapper _productMapper;
        private readonly ICategoryMapper _categoryMapper;
        public ProductService(IProductCommand productCommand, IProductQuery query, IProductMapper productMapper, ICategoryMapper categoryMapper)
        {
            _productCommand = productCommand;
            _productQuery = query;
            _productMapper = productMapper;
            _categoryMapper = categoryMapper;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
                Discount = request.Discount,
                ImageUrl = request.imageUrl
            };

            var insertedProduct = await _productCommand.InsertProduct(product);
            var productResponse = await _productMapper.GetProductResponse(insertedProduct);

            return productResponse;
        }

        public async Task<List<ProductGetResponse>> GetAll(string? name, int? limit, int? offset, int[]? categories)
        {
            List<Product> products = await _productQuery.GetListProducts(name, limit, offset, categories);
            return await _productMapper.GetProductGetResponse(products); ;
        }

        public async Task<ProductResponse> GetProductById(Guid productId)
        {
            try
            {
                if (!await CheckProductId(productId))
                {
                    throw new ExceptionNotFound("No Existe ese ID");
                }
                var product = await _productQuery.GetProduct(productId);
                var productResponse = await _productMapper.GetProductResponse(product);
                return productResponse;
            }
            catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound(ex.Message);
            }
        }

        public async Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request)
        {
            try
            {
                if (!await CheckProductId(id))
                {
                    throw new ExceptionNotFound("No Existe ese ID");
                }
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Category = request.Category,
                    Discount = request.Discount,
                    ImageUrl = request.imageUrl
                };
                var productNew = await _productCommand.UpdateProduct(product, id);
                var productResponse = await _productMapper.GetProductResponse(productNew);
                return productResponse;

            }
            catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound(ex.Message);
            }
        }

        public async Task<ProductResponse> DeleteProduct(Guid productId)
        {
            try
            {
                if (!await CheckProductId(productId))
                {
                    throw new ExceptionNotFound("No Existe ese ID");
                }
                var product = await _productQuery.GetProduct(productId);
                await _productCommand.RemoveProduct(product);
                var productResponse = await _productMapper.GetProductResponse(product);
                return productResponse;
            }
            catch (ExceptionNotFound ex) 
            {
                throw new ExceptionNotFound(ex.Message);
            }

        }

        private async Task<bool> CheckProductId(Guid id) 
        {
            return (await _productQuery.GetProduct(id) != null);
        
        }
    }
}
