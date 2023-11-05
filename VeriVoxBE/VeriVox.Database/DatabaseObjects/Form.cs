using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class Form
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string NameOnFormURL { get; set; }

        [ForeignKey("Scope")]
        public int ScopeId { get; set; }

        public Guid CreatedEntityId { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; }=false;

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("ModifiedByUser")]
        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
        public ICollection<FormQuestion> FormQuestion { get; set; }

        // Navigation property to Scopes
        public Scope Scope { get; set; }

        // Navigation property to User
        public User CreatedByUser { get; set; }

        // Navigation property to User
        public User ModifiedByUser { get; set; }


        public Form()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
