using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class UserUpdateDto
    {
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        [EmailAddress]
        public string? EmailId { get; set; } = null;
        public string? Password { get; set; } = null;
    }
}
