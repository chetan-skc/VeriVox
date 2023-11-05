using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core;
using VeriVox.Repository.Interfaces;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Diagnostics;

namespace VeriVox.Business
{
    public class CompanyService : ICompanyService
    {

        private readonly ICompanyRepository _companyService;
        public CompanyService(ICompanyRepository companyService)
        {
            _companyService = companyService;
        }

        public async Task<object> PostCompany(CompanyDto companyDto)
        {
            return await _companyService.PostCompany(companyDto);
        }

        public async Task<List<DisplayCompanyDto>> DisplayCompany()
        {
            return await _companyService.DisplayCompany();
        }

        public async Task<List<CompanyIndustries>> DisplayCompanyIndustry()
        {
            return await _companyService.DisplayCompanyIndustry();
        }

        public async Task<object> GetCompanyById(Guid id)
        {
            return await _companyService.GetCompanyById(id);
        }

        public async Task<object> DeleteCompany(Guid id)
        {
            return  await _companyService.DeleteCompany(id);
        }

        public async Task<object> UpdateCompany(Guid id, CompanyDto companyUpdateDto)
        {
            return await _companyService.UpdateCompany(id, companyUpdateDto);
        }

        public async Task<object> CompanyStateUpdate(Guid id, ActiveStateDto activedto)
        {
            return await _companyService.CompanyStateUpdate(id, activedto);
        }


    }
}
