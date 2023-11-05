using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class ModifyFormDto : IValidatableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameOnFormURL { get; set; }
        public ICollection<ModifyFormQuestionDto> FormQuestion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Name))
            {
                results.Add(new ValidationResult("Name is required.", new[] { nameof(Name) }));
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                results.Add(new ValidationResult("Description is required.", new[] { nameof(Description) }));
            }

            if (FormQuestion == null)
            {
                results.Add(new ValidationResult("Atleast one question is required.", new[] { nameof(FormQuestion) }));
            }

            return results;
        }
    }
}
