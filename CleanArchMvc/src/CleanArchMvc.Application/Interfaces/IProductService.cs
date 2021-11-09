﻿using CleanArchMvc.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<ProductDto> GetById(int? id);

        //Task<ProductDto> GetProductCategory(int? id);

        Task Add(ProductDto productDto);

        Task Update(ProductDto productDto);

        Task Remove(int? id);
    }
}