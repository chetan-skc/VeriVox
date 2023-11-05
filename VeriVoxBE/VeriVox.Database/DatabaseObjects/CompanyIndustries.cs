using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class CompanyIndustries

    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
