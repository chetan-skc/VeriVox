﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class ContactDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? Designation { get; set; }
        public bool IsAdmin { get; set; } 
        public Guid CompanyId { get; set; }
        public Guid ProductId { get; set; }
    }
}
