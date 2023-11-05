using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class FormQuestionDto
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string? Description { get; set; }
        public Boolean IsMandatory { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int QuestionTypeId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<QuestionOptionsDto> QuestionOption { get; set; }

    }
}
