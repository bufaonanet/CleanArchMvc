using CleanArchMvc.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task Add(CategoryDto categoryDto);

        Task<CategoryDto> GetById(int? id);

        Task<IEnumerable<CategoryDto>> GetCategories();

        Task Remove(int? id);

        Task Update(CategoryDto categoryDto);
    }
}