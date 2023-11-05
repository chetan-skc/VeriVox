using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Repository.Interfaces
{
    public interface IFormRepository
    {
        Task<List<Form>> GetAllAsync();
        Task<Form?> GetByIdAsync(Guid Id);
        Task<bool> CreateAsync(Form Form);
        Task<Form?> UpdateAsync(Guid Id, Form Form);
        Task<bool> DeleteAsync(Guid Id);
        Task<Form?> StatusUpdate(Guid Id, Form Form);
        Task<string> GetCreatedByAsync(Guid Id);
        Task<bool> GetOverriteAccess(Guid Id);
        Task<List<Companies>> GetAllCompany();
        Task<string> GetUrlName(Guid companyId, Guid productId, Guid formId);
        Task<bool> CreateLinkAsync(List<Link> link);
        Task<Guid> GetFormIdAsync(string token);
        Task<Guid> GetProductIdAsync(string token);
        Task<Guid> GetCompanyIdAsync(Guid productId);
        
    }
}
