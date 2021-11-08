using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));

            _mapper = mapper;
        }

        public async Task Add(ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.CreateAsync(productEntity);
        }

        public async Task<ProductDto> GetById(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<ProductDto> GetProductCategory(int? id)
        {
            var productEntity = await _productRepository.GetProductCategoryAsync(id);
            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var productEntity = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(productEntity);
        }

        public async Task Remove(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            await _productRepository.RemoveAsync(productEntity);
        }

        public async Task Update(ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(productEntity);
        }
    }
}