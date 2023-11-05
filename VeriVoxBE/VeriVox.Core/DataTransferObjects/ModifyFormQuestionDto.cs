using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class ModifyFormQuestionDto : IValidatableObject
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public Boolean IsMandatory { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int QuestionTypeId { get; set; }
        public ICollection<ModifyQuestionOptionDto> QuestionOption { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(QuestionText))
            {
                results.Add(new ValidationResult("QuestionText is required.", new[] { nameof(QuestionText) }));
            }

            if (!(IsMandatory == true || IsMandatory == false))
            {
                results.Add(new ValidationResult("'IsMandatory' must be either true or false.", new[] { nameof(IsMandatory) }));
            }

            if (Minimum < 0)
            {
                results.Add(new ValidationResult("Minimum must be greater than or equal to 0.", new[] { nameof(Minimum) }));
            }

            if (Maximum < 0)
            {
                results.Add(new ValidationResult("Maximum must be greater than 0.", new[] { nameof(Maximum) }));
            }

            if (QuestionTypeId <= 0)
            {
                results.Add(new ValidationResult("QuestionTypeId must be greater than 0.", new[] { nameof(QuestionTypeId) }));
            }

            return results;

        }
    }
}
