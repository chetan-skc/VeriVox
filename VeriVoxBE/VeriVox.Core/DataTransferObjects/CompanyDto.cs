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
    public class CompanyDto
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string LogoImage { get; set; }

        public string ShortName { get; set; }

        public Guid IndustryId { get; set; }

    }

    public class PaginatedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; }
    }


    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(200).WithMessage("Company name must be less than or equal to 200 characters.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must be less than or equal to 1000 characters.");

            RuleFor(dto => dto.ShortName)
                .NotEmpty().WithMessage("Short name is required.")
                .MaximumLength(50).WithMessage("Short name must be less than or equal to 50 characters.");

            RuleFor(dto => dto.LogoImage)
               .NotEmpty().WithMessage("LogoImage is required.");

            RuleFor(dto => dto.IndustryId)
                .NotEmpty().WithMessage("Industry ID is required.");

        }

        public List<string> ValidateAndGetErrors(CompanyDto companyDto)
        {
            var validator = new CompanyDtoValidator();
            var validationResult = validator.Validate(companyDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            }

            return null; // Return null when there are no validation errors
        }
    }


}