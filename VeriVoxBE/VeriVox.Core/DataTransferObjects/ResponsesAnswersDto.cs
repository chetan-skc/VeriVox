using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class ResponsesAnswersDto
    {
        public int ResponseId { get; set; }
        public Guid FormQuestionId { get; set; }
        public string Answer { get; set; }
    }
}