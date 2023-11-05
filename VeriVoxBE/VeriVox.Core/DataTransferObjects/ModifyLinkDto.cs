using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class ModifyLinkDto
    {

        public Guid ProductId { get; set; }
        public Guid FormId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int ResponseLimit { get; set; }
    }
}
