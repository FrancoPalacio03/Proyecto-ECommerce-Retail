using Application.Interfaces.category;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryCommand _categoryCommand;
        private readonly ICategoryQuery _categoryQuery;

        public CategoryService(ICategoryCommand command, ICategoryQuery query)
        {
            _categoryCommand = command;
            _categoryQuery = query;
        }
        public async Task CreateCategory(Category category)
        {
            await _categoryCommand.InsertCategory(category);
        }

        public async Task DeleteCategory(Category category)
        {
            await _categoryCommand.RemoveCategory(category);
        }

        public Task<List<Category>> GetAll()
        {
            return Task.FromResult(_categoryQuery.GetListCategories());
        }

        public Task<Category> GetById(int categoryId)
        {
            return Task.FromResult(_categoryQuery.GetCategory(categoryId));
        }
    }
}
