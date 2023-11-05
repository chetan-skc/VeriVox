using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class UserAddDto
    {
       public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")] 
        public required string EmailId { get; set;}
        [Required]
        public required string Password { get; set; }
    }

    public class UserValidations : AbstractValidator<UserAddDto>
    {
        public UserValidations()
        {
            RuleFor(userDto => userDto.FirstName)
            .NotEmpty().WithMessage("First Name must not be empty.")
            .MaximumLength(300).WithMessage("First Name should not exceed 300 characters.");

            RuleFor(userDto => userDto.LastName)
                .NotEmpty().WithMessage("Last Name must not be empty.")
                .MaximumLength(300).WithMessage("Last Name should not exceed 300 characters.");

            RuleFor(userDto => userDto.EmailId)
                .NotEmpty().WithMessage("Email must not be empty.")
                .MaximumLength(500).WithMessage("Email should not exceed 500 characters.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(userDto => userDto.Password)
               .NotEmpty().WithMessage("Password must not be empty.")
               .MinimumLength(8).WithMessage("Password must have at least 8 characters.")
               .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
               .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
               .Matches(@"\d").WithMessage("Password must contain at least one number.")
               .Matches(@"[@$!%*?&]").WithMessage("Password must contain at least one special character (@$!%*?&).")
               .MaximumLength(500).WithMessage("Password should not exceed 500 characters.");
        }

        public List<string>? ValidateAndGetErrors(UserAddDto userAddDto)
        {
            var validator = new UserValidations();
            var validationResult = validator.Validate(userAddDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            }

            return null; // Return null when there are no validation errors
        }

    }
    
}
