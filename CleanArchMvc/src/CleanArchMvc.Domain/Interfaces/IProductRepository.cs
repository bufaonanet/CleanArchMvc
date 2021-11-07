using CleanArchMvc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);

        Task<Product> DeleteAsync(Product product);

        Task<Product> GetByIdAsync(int? id);

        Task<IEnumerable<Product>> GetProductCategoryAsync(int? id);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> UpdateAsync(Category category);
    }
}