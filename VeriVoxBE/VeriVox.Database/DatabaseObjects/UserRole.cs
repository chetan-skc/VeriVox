using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class UserRole
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? ProductId { get; set; }
        [ForeignKey("Created")]
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("Modified")]
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
        public User? Created { get; set; }
        public User? Modified { get; set; }

    }
}
