using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Database.DatabaseObjects
{
    public class User
    {
        public Guid Id { get; set; }
        [MaxLength(300)]
        public required string FirstName { get; set; }
        [MaxLength(300)]
        public required string LastName { get; set; }
        [MaxLength(500)]
        public required string EmailId { get; set; }
        [MaxLength(500)]
        public string? Password { get; set; }
        public string? Designation { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
