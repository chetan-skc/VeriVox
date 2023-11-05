using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;

namespace VeriVox.Business
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository linkRepository;
        private readonly IMapper mapper;

        public LinkService(ILinkRepository _linkRepository, IMapper _mapper)
        {
            linkRepository = _linkRepository;
            mapper = _mapper;
        }

        public async Task<object> AddLink(LinkDto linkDTO)
        {
            
            return await linkRepository.AddLink(linkDTO);
        }

        public async Task<List<DisplayLinkDto>> GetLinkByIdAsync(Guid formid, Guid productid)
        {
            return await linkRepository.GetLinkByIdAsync(formid, productid);
        }

        public async Task<bool> DeleteLink(Guid id)
        {
            await linkRepository.DeleteLinkAsync(id);
            return true;
        }

        public async Task<List<DisplayFormDetailDto>> DisplayFormDetails(Guid id)
        {
            return await linkRepository.DisplayFormDetails(id);
        }

        public async Task<object> LinkStateUpdate(Guid id, ActiveStateDto activedto)
        {
            return await linkRepository.LinkStateUpdate(id, activedto);
        }

        public async Task<object> LinkDescriptionUpdate(Guid id, LinkUpdateDto linkUpdateDto)
        {
            return await linkRepository.LinkDescriptionUpdate(id, linkUpdateDto);
        }
    }
}
