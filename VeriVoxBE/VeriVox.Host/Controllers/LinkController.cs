using Microsoft.AspNetCore.Mvc;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using FluentAssertions.Primitives;
using VeriVox.Database.Context;

namespace VeriVox.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly CFA_DbContext cFA_DbContext;

        public LinkController(ILinkService linkService, CFA_DbContext _cFA_DbContext)
        {
            _linkService = linkService;
            cFA_DbContext = _cFA_DbContext;
        }

        [HttpGet]
        public string GetToken()
        {
            string token;
            do
            {
                token = GenerateToken();
            }
            while (IsValueInUse(token));

            return token;
        }

        private string GenerateToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool IsValueInUse(string value)
        {
            return cFA_DbContext.Links.Any(link => link.Value == value);
        }

        [HttpGet("{id}")]
        public async Task<List<DisplayLinkDto>> GetLinkById(Guid id, Guid id2)
        {
            return await _linkService.GetLinkByIdAsync(id, id2);
        }

        [Authorize]
        [HttpPost]
        public async Task<object> AddLink([FromBody] List<LinkDto> linkDtos)
        {
            foreach (var linkDto in linkDtos)
            {
                await _linkService.AddLink(linkDto);
            }
            return "Links added successfully";
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLink(Guid id)
        {
            try
            {
                await _linkService.DeleteLink(id);
                return Ok("Link deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete the link: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("form-detail/{id}")]
        public async Task<ActionResult<PaginationResult<DisplayFormDetailDto>>> DisplayFormDetails(Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 3)
        {
            var formDetails = await _linkService.DisplayFormDetails(id);

            int totalRecords = formDetails.Count;

            var paginatedFormDetails = formDetails
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginationResult = new PaginationResult<DisplayFormDetailDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = paginatedFormDetails
            };

            return Ok(paginationResult);
        }

        [HttpPut("Linkstatechange/{id}")]
        public Task<object> LinkStateUpdate(Guid id, [FromBody] ActiveStateDto activeDto)
        {
            return _linkService.LinkStateUpdate(id, activeDto);
        }

        [HttpPut("Linkdescriptionchange/{id}")]
        public Task<object> LinkDescriptionUpdate(Guid id, [FromBody] LinkUpdateDto linkUpdateDto)
        {
            return _linkService.LinkDescriptionUpdate(id, linkUpdateDto);
        }
    }
}
