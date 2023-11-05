using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace VeriVox.Core.DataTransferObjects
{
    public class ProductDto
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string LogoImage { get; set; }

        public string ShortName { get; set; }

        public Guid CompanyId { get; set; }


    }

    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(200).WithMessage("Product name must be less than or equal to 200 characters.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must be less than or equal to 1000 characters.");

            RuleFor(dto => dto.Type)
                .NotEmpty().WithMessage("Product type is required.")
                .MaximumLength(50).WithMessage("Product type must be less than or equal to 50 characters.");

            RuleFor(dto => dto.LogoImage)
                .NotEmpty().WithMessage("LogoImage is required.");

            RuleFor(dto => dto.ShortName)
                .NotEmpty().WithMessage("Short name is required.")
                .MaximumLength(50).WithMessage("Short name must be less than or equal to 50 characters.");

            RuleFor(dto => dto.CompanyId)
                .NotEmpty().WithMessage("Company ID is required.");

        }

        public List<string> ValidateAndGetErrors(ProductDto productDto)
        {
            var validator = new ProductDtoValidator();
            var validationResult = validator.Validate(productDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            }

            return null; // Return null when there are no validation errors
        }
    }
}
