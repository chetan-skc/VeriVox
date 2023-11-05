using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class FormQuestion
    {
        public Guid Id { get; set; }

        [ForeignKey("Form")]
        public Guid FormId { get; set; }
        public int QuestionNumber { get; set; }
        [MaxLength(2000)]
        public string QuestionText { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        public Boolean IsMandatory { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        [ForeignKey("QuestionType")]
        public int QuestionTypeId { get; set; }
        public Boolean IsActive { get; set; } = true;
        public Boolean IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<QuestionOption> QuestionOption { get; set; }

        //Navigation property for Forms
        public Form Form { get; set; }

        //Navigation property for QuestionType
        public QuestionType QuestionType { get; set; }

        public FormQuestion()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
