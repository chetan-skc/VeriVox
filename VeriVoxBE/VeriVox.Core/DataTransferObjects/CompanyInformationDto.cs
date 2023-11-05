using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class CompanyInformationDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Industry { get; set; }

        public Guid IndustryId { get; set; }

        public string LogoImage { get; set; }

        public string ShortName { get; set; }

        public bool IsActive { get; set; } 



    }
}
