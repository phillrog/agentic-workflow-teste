using CleanArch.Domain.Entities;
using CleanArch.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace CleanArch.Tests.Domain
{
    public class ProductTests
    {
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Negative Id")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value.");
        }

        [Fact(DisplayName = "Create Product With Short Name")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Fact(DisplayName = "Create Product With Long Image Name")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            string imageName = new string('a', 251);
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, imageName);
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image name, too long, maximum 250 characters.");
        }

        [Fact(DisplayName = "Create Product With Invalid Price")]
        public void CreateProduct_InvalidPriceValue_DomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "product image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price value.");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Pro", "Product Description", 9.99m, value, "image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock value.");
        }
    }
}