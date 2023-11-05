using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Core.Messages;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository;
using VeriVox.Repository.Interfaces;

namespace VeriVox.Business
{
    public class FormService : IFormService
    {
        private IFormRepository _formsRepository;
        private IMapper _mapper;
        private readonly ILogger<FormService> _logger;

        public FormService(IFormRepository formsRepository, IMapper mapper, ILogger<FormService> logger)
        {
            _formsRepository = formsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(ModifyFormDto addForm)
        {
            try
            {
                var formEntity = _mapper.Map<Form>(addForm);


                var createdForm = await _formsRepository.CreateAsync(formEntity);

                return createdForm;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<bool> CreateLinksAsync(List<ModifyLinkDto> links)
        {
            try
            {
                var linkEntities = _mapper.Map<List<Link>>(links);

                
                var created = await _formsRepository.CreateLinkAsync(linkEntities);

                return created;
            }
            catch (Exception ex)
            {

                _logger.LogError("An error occurred while creating links", ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var formDeleted = await _formsRepository.DeleteAsync(Id);

                if (!formDeleted)
                    throw new ApplicationException(FormMessages.FormNotFound);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<FormDto>> GetAllAsync()
        {
            try
            {
                var formDomain = await _formsRepository.GetAllAsync();
                var formDtos = _mapper.Map<List<FormDto>>(formDomain);

                return formDtos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<CompanyDto>> GetAllCompanyAsync()
        {
            try
            {
                var companyDomain = await _formsRepository.GetAllCompany();
                var companyDto = _mapper.Map<List<CompanyDto>>(companyDomain);
                return companyDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FormDto?> GetByIdAsync(Guid Id)
        {
            try
            {
                var formDomain = await _formsRepository.GetByIdAsync(Id);

                if (formDomain == null)
                    return null;

                var formDto = _mapper.Map<FormDto>(formDomain);

                return formDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> GetCreatedByAsync(Guid Id)
        {
            try
            {
                var createdBy = await _formsRepository.GetCreatedByAsync(Id);

                if (createdBy==null)
                {
                    return null;
                }
                return createdBy;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> GetOverriteAccess(Guid Id)
        {
            try
            {
                var access = await _formsRepository.GetOverriteAccess(Id);
                return access;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> GetUrlName(Guid companyId, Guid productId, Guid formId)
        {
            try
            {
                string link = await _formsRepository.GetUrlName(companyId, productId, formId);
                return link;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActiveStateDto?> StatusUpdate(Guid Id, ActiveStateDto activeStateDto)
        {
            try
            {
                var formDomainModel = _mapper.Map<Form>(activeStateDto);

                var existingForm = await _formsRepository.StatusUpdate(Id,formDomainModel);

                if(existingForm == null)
                    throw new ApplicationException(FormMessages.FormNotFound);
                

                return _mapper.Map<ActiveStateDto>(existingForm); ;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        public async Task<bool> UpdateAsync(Guid Id, ModifyFormDto updateFormsDto)
        {
            try
            {
                
                var formDomainModel = _mapper.Map<Form>(updateFormsDto);

                
                var existingForm = await _formsRepository.UpdateAsync(Id, formDomainModel);

                if (existingForm == null)
                    throw new ApplicationException(FormMessages.FormNotFound);

                

                await _formsRepository.UpdateAsync(Id, existingForm);

                return true;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<Guid> GetFormIdAsync(string token)
        {
            try
            {
                return await _formsRepository.GetFormIdAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching FormId", ex);
                return Guid.Empty;
            }
        }

        public async Task<Guid> GetProductIdAsync(string token)
        {
            try
            {
                return await _formsRepository.GetProductIdAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching ProductId", ex);
                return Guid.Empty;
            }
        }

        public async Task<Guid> GetCompanyIdAsync(Guid productId)
        {
            try
            {
                return await _formsRepository.GetCompanyIdAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching CompanyId", ex);
                return Guid.Empty;
            }
        }

       
    }
}
