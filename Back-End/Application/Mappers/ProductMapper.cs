using Application.Interfaces.category;
using Application.Interfaces.IMappers;
using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class ProductMapper : IProductMapper
    {
        private readonly ICategoryMapper _categoryMapper;

        public ProductMapper(ICategoryMapper categoryMapper) 
        {
            _categoryMapper = categoryMapper;
        }
        public async Task<ProductResponse> GetProductResponse(Product product)
        {
            var response = new ProductResponse
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                imageUrl = product.ImageUrl,
                categoryResponse = await _categoryMapper.GetCategoryResponse(product.category),
            };
            return await Task.FromResult(response);
        }

        public async Task<List<ProductGetResponse>> GetProductGetResponse(List<Product> products)
        {
            List<ProductGetResponse> productGetResponses = new List<ProductGetResponse>();

            foreach (var product in products)
            {
                var category = await _categoryMapper.GetCategoryResponse(product.category);
                var response = new ProductGetResponse
                {
                    Id = product.ProductId.ToString(),
                    Name = product.Name,
                    Price = product.Price,
                    Discount = product.Discount,
                    imageUrl = product.ImageUrl,
                    CategoryName = category.Name,
                };
                productGetResponses.Add(response);
            }
            return await Task.FromResult(productGetResponses);
        }

    }
}
