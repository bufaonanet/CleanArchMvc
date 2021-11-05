﻿using CleanArchMvc.Domain.Validation;
using System.Collections.Generic;

namespace CleanArchMvc.Domain.Entities
{

    public sealed class Product : Entity
    {   
        
        public string Name { get; private set; }
        public string Descrition { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public int CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }

        public Product(int id, string name, string descrition, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id");
            Id = id;
            ValidateDomain(name, descrition, price, stock, image);
        }

        public Product(string name, string descrition, decimal price, int stock, string image)
        {
            ValidateDomain(name, descrition, price,stock,image);           
        }

        public void Update(string name, string descrition, decimal price, int stock, string image, int catetoryId)
        {
            ValidateDomain(name, descrition, price, stock, image);
            CategoryId = catetoryId;
        }

        private void ValidateDomain(string name, string descrition, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required!");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name. Minium  3 caracteres!");

            DomainExceptionValidation.When(string.IsNullOrEmpty(descrition),
                "Invalid descrition. descrition is required!");

            DomainExceptionValidation.When(descrition.Length < 3,
                "Invalid descrition. Minium  3 caracteres!");

            DomainExceptionValidation.When(price < 0, "Invalid price value.");

            DomainExceptionValidation.When(stock < 0, "Invalid price value.");

            DomainExceptionValidation.When(image.Length > 250,
                "Invalid image. Maximum 250 caracteres!");

            Name = name;
            Descrition = descrition;
            Price = price;
            Stock = stock;
            Image = image;
        }
    }
}