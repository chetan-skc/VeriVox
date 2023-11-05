using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class QuestionOptionsDto
    {
        public Guid Id { get; set; }
        public Guid FormQuestionId { get; set; }
        public int OptionOrder { get; set; }
        public string OptionText { get; set; }
        public string OptionValue { get; set; }
    }
}
