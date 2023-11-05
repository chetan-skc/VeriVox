using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Business.Interfaces
{
    public interface ILinkService
    {
        Task<object> AddLink(LinkDto linkDTO);
        Task<List<DisplayLinkDto>> GetLinkByIdAsync(Guid formid, Guid productid);
        Task<bool> DeleteLink(Guid id);
        Task<List<DisplayFormDetailDto>> DisplayFormDetails(Guid id);
        Task<object> LinkStateUpdate(Guid id, ActiveStateDto activedto);
        Task<object> LinkDescriptionUpdate(Guid id, LinkUpdateDto linkUpdateDto);
    }
}
