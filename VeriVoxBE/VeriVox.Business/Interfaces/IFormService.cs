using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;


namespace VeriVox.Business.Interfaces
{
    public interface IFormService
    {
        Task<List<FormDto>> GetAllAsync();
        Task<FormDto?> GetByIdAsync(Guid Id);
        Task<bool> CreateAsync(ModifyFormDto addForm);
        Task<bool> UpdateAsync(Guid Id, ModifyFormDto updateFormsDto);
        Task<bool> DeleteAsync(Guid Id);
        Task<ActiveStateDto?> StatusUpdate(Guid Id, ActiveStateDto activeStateDto);
        Task<string> GetCreatedByAsync(Guid Id);
        Task<bool> GetOverriteAccess(Guid Id);
        Task<List<CompanyDto>> GetAllCompanyAsync();
        Task<string> GetUrlName(Guid companyId, Guid productId, Guid formId);
        Task<bool> CreateLinksAsync(List<ModifyLinkDto> links);
        Task<Guid> GetFormIdAsync(string token);
        Task<Guid> GetProductIdAsync(string token);
        Task<Guid> GetCompanyIdAsync(Guid productId);
        
    }
}
