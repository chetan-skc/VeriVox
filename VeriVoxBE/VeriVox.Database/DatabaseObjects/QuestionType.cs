using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class QuestionType
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }
}
