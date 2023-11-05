using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VeriVox.Business;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Core.Messages;

namespace VeriVox.Host.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : Controller
    {
        private readonly IFormService _formService;
        private readonly ILogger<FormController> _logger;

        public FormController(IFormService formService , ILogger<FormController> logger)
        {
            _formService = formService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<List<FormDto>>> GetAllForms()
        {
            var forms = await _formService.GetAllAsync();
            return Ok(forms);
            
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<FormDto>> GetFormById(Guid Id)
        {
            var form = await _formService.GetByIdAsync(Id);
            if (form == null)
            {
                return NotFound(FormMessages.FormNotFound);
            }

            return Ok(form);
        }

        [HttpPost]
        public async Task<ActionResult<FormDto>> CreateForm([FromBody] ModifyFormDto addFormsDto)
        {
            var CreatedForm = await _formService.CreateAsync(addFormsDto);
            return Ok(FormMessages.FormCreatedSuccess);
        }

        //[Authorize(Policy = "UpdateForm")]
        [HttpPut("{Id}")]
        public async Task<ActionResult<FormDto>> UpdateForm(Guid Id, [FromBody] ModifyFormDto updateFormsDto)
        {
            var updatedForm = await _formService.UpdateAsync(Id, updateFormsDto);
            if (updatedForm == null)
            {
                return NotFound(FormMessages.FormNotFound);
            }
            return Ok(FormMessages.FormUpdatedSuccess);

        }

        //[Authorize(Policy = "DeleteForm")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteForm(Guid Id)
        {
            var deletedForm = await _formService.DeleteAsync(Id);
            if (!deletedForm)
            {
                return NotFound(FormMessages.FormNotFound);
            }
            return Ok(FormMessages.FormDeletedSuccess);
        }

        [HttpPut("FormStatusChange{Id}")]
        public async Task<ActionResult<ActiveStateDto>> StatusUpdate(Guid Id,[FromBody] ActiveStateDto activeStateDto)
        {
            var updatedState = await _formService.StatusUpdate(Id, activeStateDto);

            if(updatedState == null)
            {
                return NotFound(FormMessages.FormNotFound);
            }

            return Ok(updatedState);
        }

        [HttpGet("GetCreatedBy/{Id}")]
        public async Task<ActionResult<string>> GetCreatedBy(Guid Id)
        {
            var createdBy = await _formService.GetCreatedByAsync(Id);
            return Ok(createdBy);
        }

        [HttpGet("GetAccess/{Id}")]
        public async Task<ActionResult<bool>> GetOverriteAccess(Guid Id)
        {
            var access =await _formService.GetOverriteAccess(Id);
            return Ok(access);
        }

        [HttpGet("GetAllCompanies")]
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
        {
            var companies = await _formService.GetAllCompanyAsync();
            return Ok(companies);
        }

        [HttpGet("GetUrlText")]
        public async Task<ActionResult<string>> GetUrlText(Guid companyId, Guid productId, Guid formId)
        {
            var link = await _formService.GetUrlName(companyId, productId, formId);
            return Ok(link);
        }

        [HttpPost("createLinks")]
        public async Task<IActionResult> CreateLinks([FromBody] List<ModifyLinkDto> links)
        {
            try
            {
                var created = await _formService.CreateLinksAsync(links);

                if (created)
                {
                    return Ok("Links created successfully.");
                }
                else
                {
                    return BadRequest("Failed to create links.");
                }
            }
            catch (Exception ex)
            {
                
                _logger.LogError("An error occurred while processing the request", ex);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("GetFormId")]
        public async Task<ActionResult<Guid>> GetFormIdAsync(string token)
        {
            try
            {
                return await _formService.GetFormIdAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request", ex);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("GetProductId")]
        public async Task<ActionResult<Guid>> GetProductIdAsync(string token)
        {
            try
            {
                return await _formService.GetProductIdAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request", ex);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("GetCompanyId")]
        public async Task<ActionResult<Guid>> GetCompanyIdAsync(Guid productId)
        {
            try
            {
                return await _formService.GetCompanyIdAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request", ex);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        
    }
}
