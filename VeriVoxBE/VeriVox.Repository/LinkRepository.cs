using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;

public class LinkRepository : ILinkRepository
{
    private readonly CFA_DbContext cFA_DbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Mapper mapper;

    public LinkRepository(CFA_DbContext _cFA_DbContext, IHttpContextAccessor httpContextAccessor)
    {
        cFA_DbContext = _cFA_DbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<object> AddLink(LinkDto linkDto)
    {
        var userClaims = _httpContextAccessor.HttpContext.User.Claims;
        var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "Id");
        string userId = userIdClaim.Value;
        Guid userGuid = Guid.Parse(userId);
        




        var result = new Link
        {
            ProductId = linkDto.ProductId,
            FormId = linkDto.FormId,
            Description = linkDto.Description,
            Value = linkDto.Value,
            ResponseLimit = linkDto.ResponseLimit,
            CreatedBy = userGuid,
            ModifiedBy = userGuid
        };
        await cFA_DbContext.Links.AddAsync(result);
        await cFA_DbContext.SaveChangesAsync();
        var message = new { Message = "True" };
        return message;
    }

    public async Task<List<DisplayLinkDto>> GetLinkByIdAsync(Guid formid, Guid productid)
    {
        var form = cFA_DbContext.Form.FirstOrDefault(x => x.Id == formid);
        var product = cFA_DbContext.Form.FirstOrDefault(x => x.Id == productid);

        var query = from links in cFA_DbContext.Links.Where(frm => frm.FormId == formid && frm.ProductId == productid)
                    join forms in cFA_DbContext.Form on links.FormId equals forms.Id
                    join products in cFA_DbContext.Products on links.ProductId equals products.Id
                    join company in cFA_DbContext.Companies on products.CompanyId equals company.Id
                    select new DisplayLinkDto
                    {
                        Id = links.Id,
                        form = forms.NameOnFormURL,
                        company = company.Name,
                        companyshort = company.ShortName,
                        product = products.Name,
                        productshort = products.ShortName,
                        value = links.Value,
                        description = links.Description,
                        IsActive = links.IsActive,
                        IsDeleted = links.IsDeleted
                    };
        return await query.ToListAsync();
    }

    public async Task<Link> DeleteLinkAsync(Guid id)
    {
        var existingLink = await cFA_DbContext.Links.FirstOrDefaultAsync(l => l.Id == id);

        if (existingLink != null)
        {
            cFA_DbContext.Links.Remove(existingLink);
            await cFA_DbContext.SaveChangesAsync();
            return existingLink;
        }
        else
        {
            throw new Exception("Link not found");
        }
    }

    public async Task<List<DisplayFormDetailDto>> DisplayFormDetails(Guid id)
    {
        var userRolesTable = cFA_DbContext.UserRoles;
        var userClaims = _httpContextAccessor.HttpContext.User.Claims;
        var userRole = userClaims.FirstOrDefault(c => c.Type == "Role")?.Value;
        var userId = userClaims.FirstOrDefault(c => c.Type == "Id")?.Value;
        Guid userGuid = Guid.Parse(userId);
        var IsUserInUserRoleTable = cFA_DbContext.UserRoles.FirstOrDefault(x => x.UserId == userGuid);

        if (userRole == "1" || userRole == "2")
        {
            var query = from forms in cFA_DbContext.Form.Where(frm => frm.Id == id)
                        join links in cFA_DbContext.Links on forms.Id equals links.FormId
                        join product in cFA_DbContext.Products on links.ProductId equals product.Id
                        join company in cFA_DbContext.Companies on product.CompanyId equals company.Id
                        group new { forms, product, company, links } by product.Id into grouped
                        select new DisplayFormDetailDto
                        {
                            Id = grouped.Key,
                            Name = grouped.First().forms.Name,
                            Description = grouped.First().forms.Description,
                            Company = grouped.First().company.Name,
                            CompanyShort = grouped.First().company.ShortName,
                            companylogo = grouped.First().company.LogoImage,
                            productId = grouped.First().product.Id,
                            Product = grouped.First().product.Name,
                            ProductShort = grouped.First().product.ShortName,
                            productlogo = grouped.First().product.LogoImage,
                            ShortName = grouped.First().forms.NameOnFormURL,
                            NoOfLinks = grouped.Count(item => item.links.ProductId == grouped.Key),
                            IsActive = grouped.Any(item => item.links.IsActive)
                        };

            return await query.ToListAsync();
        }

        else
        {
            var query = from forms in cFA_DbContext.Form.Where(frm => frm.Id == id)
                        join links in cFA_DbContext.Links on forms.Id equals links.FormId
                        join product in cFA_DbContext.Products on links.ProductId equals product.Id
                        join company in cFA_DbContext.Companies on product.CompanyId equals company.Id
                        where company.Id == IsUserInUserRoleTable.CompanyId
                        group new { forms, product, company, links } by product.Id into grouped
                        select new DisplayFormDetailDto
                        {
                            Id = grouped.Key,
                            Name = grouped.First().forms.Name,
                            Description = grouped.First().forms.Description,
                            Company = grouped.First().company.Name,
                            CompanyShort = grouped.First().company.ShortName,
                            companylogo = grouped.First().company.LogoImage,
                            productId = grouped.First().product.Id,
                            Product = grouped.First().product.Name,
                            ProductShort = grouped.First().product.ShortName,
                            productlogo = grouped.First().product.LogoImage,
                            ShortName = grouped.First().forms.NameOnFormURL,
                            NoOfLinks = grouped.Count(item => item.links.ProductId == grouped.Key),
                            IsActive = grouped.Any(item => item.links.IsActive)
                        };

            return await query.ToListAsync();
        }
    }

    public async Task<object> LinkStateUpdate(Guid id, ActiveStateDto activedto)
    {
        var active = new ActiveStateDto
        {
            IsActive = activedto.IsActive,
        };

        var updateLink = cFA_DbContext.Links.SingleOrDefault(x => x.Id == id);
        if (updateLink == null)
        {
            var result = new { Message = "Link change state failed" };
            return result;
        }
        else
        {
            updateLink.IsActive = activedto.IsActive;
            cFA_DbContext.SaveChanges();
            var result = new { Message = "Link state change successfully" };
            return result;
        }

    }

    public async Task<object> LinkDescriptionUpdate(Guid id, LinkUpdateDto linkUpdateDto)
    {
        var active = new LinkUpdateDto
        {
            description = linkUpdateDto.description,
        };

        var updateLink = cFA_DbContext.Links.SingleOrDefault(x => x.Id == id);
        if (updateLink == null)
        {
            var result = new { Message = "Link description change failed" };
            return result;
        }
        else
        {
            updateLink.Description = linkUpdateDto.description;
            cFA_DbContext.SaveChanges();
            var result = new { Message = "Link description change successfully" };
            return result;
        }

    }
}
