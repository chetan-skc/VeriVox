using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DatabaseObjects
{
    public class Companies

    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("CompanyIndustry")]
        public Guid IndustryId { get; set; }

        public string LogoImage { get; set; }

        [MaxLength(20)]
        public string ShortName { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Created")]

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("Modified")]

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public CompanyIndustries CompanyIndustry { get; set; }

        public User Created { get; set; }

        public User Modified { get; set; }
    }
}
