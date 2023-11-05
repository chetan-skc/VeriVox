using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Repository.Interfaces
{
    public interface ILinkRepository
    {
        Task<object> AddLink(LinkDto link);
        Task<List<DisplayLinkDto>> GetLinkByIdAsync(Guid formid, Guid productid);
        Task<Link> DeleteLinkAsync(Guid id);
        Task<List<DisplayFormDetailDto>> DisplayFormDetails(Guid id);
        Task<object> LinkStateUpdate(Guid id, ActiveStateDto activedto);
        Task<object> LinkDescriptionUpdate(Guid id, LinkUpdateDto linkUpdateDto);
    }
}
