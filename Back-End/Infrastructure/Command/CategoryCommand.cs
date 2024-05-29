using Application.Interfaces.category;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class CategoryCommand : ICategoryCommand
    {
        private readonly AppDbContext _context;

        public CategoryCommand(AppDbContext context) 
        {
            context = _context;
        }

        public async Task InsertCategory(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCategory(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }
    }
}
