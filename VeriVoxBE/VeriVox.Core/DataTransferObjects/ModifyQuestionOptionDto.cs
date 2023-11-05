using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class ModifyQuestionOptionDto
    {
        public int OptionOrder { get; set; }
        public string OptionText { get; set; }
        public string OptionValue { get; set; }
    }
}
