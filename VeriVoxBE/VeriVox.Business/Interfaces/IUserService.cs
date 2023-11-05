using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Core.Messages;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Business.Interfaces
{
    public interface IUserService
    {
        Task<object> AddUser(UserAddDto userAddDto);
        Task<List<UserGetDto>> GetUsers();
        Task<object?> DeleteUser(string email);
        Task<object?> UpdateUser(Guid id, UserUpdateDto userUpdateDto);
        Task<string> Login(LoginData loginData);
        Task<object?> AddContact(ContactDto contactDto);
        Task<List<ContactInfoDto>> GetContacts(Guid companyId);
        Task<object?> DeleteContact(string email);
        Task<object?> UpdateContact(string email, UpdateContactDto updatecontactDto);
        Task<object?> AddProductContact(ProductContactDto productContactDto);
        Task<List<ContactInfoDto>> GetProductContact(Guid productId);
        Task<string> GoogleLogin(string email);
    }
}
