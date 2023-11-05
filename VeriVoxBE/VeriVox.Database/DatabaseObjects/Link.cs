using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class Link
    {
        public Guid Id { get; set; }
        [ForeignKey("ProductKey")]
        public Guid ProductId { get; set; }
        [ForeignKey("Formskey")]
        public Guid FormId { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string Value { get; set; }
        public int ResponseLimit { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("Created")]
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("Modified")]
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public Products ProductKey { get; set; }
        public Form Formskey { get; set; }
        public User Created { get; set; }
        public User Modified { get; set; }
    }
}
