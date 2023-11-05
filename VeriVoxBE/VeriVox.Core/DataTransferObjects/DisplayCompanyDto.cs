using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class DisplayCompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; } 
        public List<string> Products { get; set; }
    }
}
