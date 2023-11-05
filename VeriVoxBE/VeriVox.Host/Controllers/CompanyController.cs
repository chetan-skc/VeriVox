using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks.Dataflow;
using VeriVox.Business;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static VeriVox.Core.DataTransferObjects.CompanyDto;
using static VeriVox.Core.DataTransferObjects.CompanyUpdateDto;


namespace VeriVox.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [Authorize(Policy = "CreateCompany")]
        [HttpPost]
        public async Task<ActionResult> PostCompany( CompanyDto companyDto)
        {
            var validator = new CompanyDtoValidator();
            var validationErrors = validator.ValidateAndGetErrors(companyDto);

            

            if (validationErrors != null && validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            await _companyService.PostCompany(companyDto);

            return Ok("Company created successfully.");
        }
        [Authorize(Policy = "ViewCompany")]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<DisplayCompanyDto>>> DisplayCompany(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 7)
        {
            var companies = await _companyService.DisplayCompany();

            int totalRecords = companies.Count;

            var paginatedCompanies = companies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginatedResult = new PaginatedResult<DisplayCompanyDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = paginatedCompanies
            };

            return Ok(paginatedResult);
        }
       
        [HttpGet("companyindustry/")]
        public async Task<List<CompanyIndustries>> DisplayCompanyIndustry()
        {
            return await _companyService.DisplayCompanyIndustry();
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Companies>>> GetCompanyById(Guid id)
        {
            var company = await _companyService.GetCompanyById(id);
            if(company == null)
            {
                return NotFound();
            }
            return Ok(company);
            
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<object> DeleteCompany(Guid id)
        {
           return await _companyService.DeleteCompany(id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public  IActionResult UpdateCompany(Guid id,[FromBody] CompanyDto companyUpdateDto)
        {
            var validator = new CompanyUpdateDtoValidator();
            var validationErrors = validator.ValidateAndGetErrors(companyUpdateDto);

            if (validationErrors != null && validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            var result =_companyService.UpdateCompany(id, companyUpdateDto);

            if (!result.IsCompleted)
            {
                return BadRequest("Company could not be updated.");
            }

            return Ok("Company updated successfully.");
            
        }
        [Authorize]
        [HttpPut("companystatechange/{id}")]
        public Task<object> CompanyStateUpdate(Guid id, [FromBody] ActiveStateDto activeDto)
        {
            return _companyService.CompanyStateUpdate(id, activeDto);
            

        }
    }
}
