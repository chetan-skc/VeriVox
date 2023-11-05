using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Business.Interfaces
{
    public interface ICompanyService
    {
        Task<object> PostCompany(CompanyDto companydto);
        Task<List<DisplayCompanyDto>> DisplayCompany();

        Task<List<CompanyIndustries>> DisplayCompanyIndustry();

        Task<object> GetCompanyById(Guid id);

        Task<object> DeleteCompany(Guid id);

        Task<object> UpdateCompany(Guid id, CompanyDto companyUpdateDto);

        Task<object> CompanyStateUpdate(Guid id, ActiveStateDto activedto);

    }
}
