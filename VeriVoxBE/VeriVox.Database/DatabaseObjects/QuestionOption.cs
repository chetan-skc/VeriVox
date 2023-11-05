using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class QuestionOption
    {
        public Guid Id { get; set; }

        [ForeignKey("FormQuestion")]
        public Guid FormQuestionId { get; set; }
        public int OptionOrder { get; set; }
        public string OptionText { get; set; }
        public string OptionValue { get; set; }

        //Navigate property to FormQuestion
        public FormQuestion FormQuestion { get; set; }
    }
}
