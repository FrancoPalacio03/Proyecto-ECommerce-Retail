using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.category
{
    public interface ICategoryService
    {
        Task CreateCategory(Category category);
        Task DeleteCategory(Category category);
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
    }
}
