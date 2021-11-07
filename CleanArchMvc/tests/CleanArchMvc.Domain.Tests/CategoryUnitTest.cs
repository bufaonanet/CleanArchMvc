using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Criando categoria válida")]
        public void CreateCategory_WithValidParameters_ResultValidObject()
        {
            Action action = () => new Category(1, "Category name");

            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criando categoria com id negativo")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category name");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id!");
        }

        [Fact(DisplayName = "Criando categoria nome curto")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");

            action.Should()
                .Throw<DomainExceptionValidation>()
                   .WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Criando categoria nome vazio")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name.Name is required");
        }

        [Fact(DisplayName = "Criando categoria nome vazio")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}