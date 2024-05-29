using Application.Interfaces.IMappers;
using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public async Task<CategoryResponse> GetCategoryResponse(Category category)
        {
            var categoryResponse = new CategoryResponse
            {
                Id = category.CategoryId,
                Name = category.Name
            };
            return await Task.FromResult(categoryResponse);
        }
    }
}
