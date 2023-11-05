using AutoMapper;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;

namespace VeriVox.Repository
{
    public class CompanyRepository : ICompanyRepository

    {
        private readonly CFA_DbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public CompanyRepository(CFA_DbContext dbContext,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> PostCompany(CompanyDto companyDto)
        {
            var company =  _mapper.Map<Companies>(companyDto);

            var userClaims = _httpContextAccessor.HttpContext.User.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "Id");
            string userId = userIdClaim.Value;
            Guid userGuid = Guid.Parse(userId);
            company.CreatedBy = userGuid;
            company.ModifiedBy = userGuid;
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();

            var result = new { Message = "Company added successfully", CompanyId = company.Id };
            return Task.FromResult<object>(result);
        }

        public async Task<List<DisplayCompanyDto>> DisplayCompany()
        {
            var userRolesTable = _dbContext.UserRoles;
            var userClaims = _httpContextAccessor.HttpContext.User.Claims;
            var userRole = userClaims.FirstOrDefault(c => c.Type == "Role")?.Value;
            var userId = userClaims.FirstOrDefault(c => c.Type == "Id")?.Value;
            Guid userGuid = Guid.Parse(userId);
            var IsUserInUserRoleTable = _dbContext.UserRoles.FirstOrDefault(x => x.UserId == userGuid);

            if (userRole=="1"||userRole=="2")
            {
                var query = from company in _dbContext.Companies
                            join industry in _dbContext.CompanyIndustries on company.IndustryId equals industry.Id
                            join createdByUser in _dbContext.Users on company.CreatedBy equals createdByUser.Id
                            join modifiedByUser in _dbContext.Users on company.ModifiedBy equals modifiedByUser.Id
                            where !company.IsDeleted
                            select new DisplayCompanyDto
                            {
                                Id = company.Id,
                                Name = company.Name,
                                Industry = industry.Name,
                                IsActive = company.IsActive,
                                CreatedByName = createdByUser.FirstName + " " + createdByUser.LastName,
                                CreatedDate = company.CreatedDate,
                                Products = (from product in _dbContext.Products
                                            where product.CompanyId == company.Id && product.IsDeleted == false
                                            select product.Name).ToList()

                            };
                query = query.OrderByDescending(c => c.CreatedDate);

                return query.ToList();
            }
            else 
            {
                if(IsUserInUserRoleTable == null)
                {
                    return null;
                }
                else
                {
                    var query = from company in _dbContext.Companies
                                join industry in _dbContext.CompanyIndustries on company.IndustryId equals industry.Id
                                join createdByUser in _dbContext.Users on company.CreatedBy equals createdByUser.Id
                                where company.Id == IsUserInUserRoleTable.CompanyId
                                select new DisplayCompanyDto
                                {
                                    Id = company.Id,
                                    Name = company.Name,
                                    Industry = industry.Name,
                                    IsActive = company.IsActive,
                                    CreatedByName = createdByUser.FirstName + " " + createdByUser.LastName,
                                    CreatedDate = company.CreatedDate,
                                    Products = (from product in _dbContext.Products
                                                where product.CompanyId == company.Id && product.IsDeleted == false
                                                select product.Name).ToList()

                                };
                    return query.ToList();
                }
            }

            
        }


        public async Task<List<CompanyIndustries>> DisplayCompanyIndustry()
        {
            return _dbContext.CompanyIndustries.ToList();
        }

        public async Task<object> GetCompanyById(Guid id)
        {
            var query = from company in _dbContext.Companies
                        join industry in _dbContext.CompanyIndustries on company.IndustryId equals industry.Id
                        where company.Id == id
                        select new CompanyInformationDto
                        {
                            Id = company.Id,
                            Name = company.Name,
                            LogoImage = company.LogoImage,
                            Industry = industry.Name,
                            IndustryId = company.IndustryId,
                            Description = company.Description,
                            ShortName= company.ShortName,
                            IsActive = company.IsActive,
                        };
            var data = query.FirstOrDefault();
            if (data == null)
            {

                var result = new { Message = "Company not Found" };
                return result;
            }
            return data;
        }


        public async Task<object> DeleteCompany(Guid id)
        {

            var company = _dbContext.Companies.SingleOrDefault(x => x.Id == id);
            if (company == null)
            {
                var result = new { Message = "Company not Deleted"};
                return result;
            }
            else
            {
                company.IsDeleted = true;
                _dbContext.SaveChanges();
                var result = new { Message = "Company Deleted successfully" };
                return result;

            }

        }

        public async Task<object> UpdateCompany(Guid id, CompanyDto companyUpdateDto)
        {

            var updatecompany = _dbContext.Companies.SingleOrDefault(x => x.Id == id);
            if (updatecompany == null)
            {
                var result = new { Message = "Company not Updated" };
                return result;
            }
            else
            {
                _mapper.Map(companyUpdateDto, updatecompany);
                _dbContext.SaveChanges();
                var result = new { Message = "Company Updated" };
                return result;
            }

        }


        public async Task<object> CompanyStateUpdate(Guid id, ActiveStateDto activedto)
        {
            var updatecompany = _dbContext.Companies.SingleOrDefault(x => x.Id == id);
            if (updatecompany == null)
            {
                var result = new { Message = "Company change state failed" };
                return result;

            }
            else
            {
                updatecompany.IsActive = activedto.IsActive;
                _dbContext.SaveChanges();
                var result = new { Message = "Company state change successfully" };
                return result;
            }

        }


    }
}
