using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.DataTransferObjects
{
    public class DisplayFormDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string CompanyShort { get; set; }
        public string companylogo { get; set; }
        public Guid productId { get; set; }
        public string Product { get; set; }
        public string ProductShort { get; set; }
        public string productlogo { get; set; }
        public string ShortName { get; set; }
        public int NoOfLinks { get; set; }
        public bool IsActive { get; set; }
    }

    public class PaginationResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; }
    }
}

