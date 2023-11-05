using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class ResponseAnswers
    {
        public Guid Id { get; set; }
        public int ResponseId { get; set; }
        [ForeignKey("FormQuestionID")]
        public Guid FormQuestionId { get; set; }
        [MaxLength(4000)]
        public string Answer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public FormQuestion FormQuestionID { get; set; }
    }
}
