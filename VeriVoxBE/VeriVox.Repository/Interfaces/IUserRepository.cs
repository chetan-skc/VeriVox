using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<object> AddContact(User user);
        Task<List<User>> GetUsers();
        Task<User?> DeleteUser(string email);
        Task<object> UpdateUser(Guid id, User user);
        Task<User?> GetUserByEmail(string email);
        Task<UserRole> GetUserRoleById(Guid id);
        Task<object> AddUser(User user);
        Task<object> AddUserAsAdmin(UserRole user);
        Task<List<ContactInfoDto>> GetContacts(Guid companyId);
        Task<object> DeleteContact(string email);
        Task<object?> UpdateContact(string email, UpdateContactDto updatecontactDto);
        Task<object?> AddProductContact(User user);
        Task<List<ContactInfoDto>> GetProductContact(Guid productId);
    }
}
