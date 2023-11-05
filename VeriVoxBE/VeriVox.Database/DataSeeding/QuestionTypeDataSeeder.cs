using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;
using static System.Formats.Asn1.AsnWriter;

namespace VeriVox.Database.DataSeeding
{
    public static class QuestionTypeDataSeeder
    {
        public static void QuestionTypeSeedData(this ModelBuilder modelBuilder)
        {
            var questionType = new List<QuestionType>
            {
                    new QuestionType { Id = 1, Name = "Short Text"},
                    new QuestionType { Id = 2, Name = "Number Input"},
                    new QuestionType { Id = 3, Name = "Big Text", Minimum = 100, Maximum= 4000},
                    new QuestionType { Id = 4, Name = "Ratings", Minimum = 3, Maximum = 10 },
                    new QuestionType { Id = 5, Name = "Dropdown", Minimum = 1, Maximum = 20 },
                    new QuestionType { Id = 6, Name = "RadioButtons", Minimum = 1, Maximum = 10 },
                    new QuestionType { Id = 7, Name = "TypeAhead", Minimum = 1, Maximum = 100 },
                    new QuestionType { Id = 8, Name = "CheckBox", Minimum = 1, Maximum = 10 }
            };

            modelBuilder.Entity<QuestionType>().HasData(questionType);
        }

        
        
    }
}
