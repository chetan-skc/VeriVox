using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class LinkDto
    {
        public Guid ProductId { get; set; }
        public Guid FormId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int ResponseLimit { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
    public class LinkDtoValidator : AbstractValidator<LinkDto>
    {
        public LinkDtoValidator()
        {
            RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description must not be empty.")
            .MaximumLength(500).WithMessage("First Name should not exceed 300 characters.");
        }
    }
}