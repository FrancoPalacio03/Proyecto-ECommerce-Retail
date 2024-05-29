using Application.Request;
using Application.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.product
{
    public interface IProductCommand
    {
        Task<Product> InsertProduct(Product product);
        Task<Product> RemoveProduct(Product product);
        Task<Product> UpdateProduct(Product product,Guid id);

    }
}
