using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class DisplayLinkDto
    {
        public Guid Id { get; set; }
        public string value { get; set; }
        public string form {  get; set; }
        public string company { get; set; }
        public string companyshort {  get; set; }
        public string product { get; set; }
        public string productshort { get; set; }
        public string description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
