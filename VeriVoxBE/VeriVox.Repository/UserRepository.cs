using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Core.Messages;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;

namespace VeriVox.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CFA_DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserMessages _userMessages;
        public UserRepository(CFA_DbContext dbContext, IMapper mapper, UserMessages userMessages)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userMessages = userMessages;
        }


        public async Task<object> AddUser(User user)
        {
            var existinguser = await _dbContext.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId);
            if (existinguser == null)
            {

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return _userMessages.UserRegisterSuccess;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }


        public async Task<User?> DeleteUser(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.EmailId == email);
            if (user == null)
            {
               return null;
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return user;
        }

        public async Task<object> UpdateUser(Guid id, User user)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            bool flag = false;
            if (_user == null)
            {
                return _userMessages.UserNotExist;
            }
            if (user.FirstName!= "string" && _user.FirstName != user.FirstName)
            {
                _user.FirstName = user.FirstName;
                flag = true;
            }

            if (user.LastName != "string" && _user.LastName != user.LastName)
            {
                _user.LastName = user.LastName;
                flag = true;
            }

            if (user.EmailId != "user@example.com" && _user.EmailId != user.EmailId)
            {
                _user.EmailId = user.EmailId;
                flag = true;
            }

            if (user.Password != "string" && _user.Password != user.Password)
            {
                _user.Password = user.Password;
                flag = true;
            }
            if(flag == false)
            {
                    return _userMessages.NothingToUpdate;
            }
            else
            {
                await _dbContext.SaveChangesAsync();
                _mapper.Map<User, UserGetDto>(_user);
                return _userMessages.UserUpdated;
            }
            
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.EmailId == Email && !x.IsDeleted);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<UserRole> GetUserRoleById(Guid id)
        {
            var user = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == id);
            if(user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<object> AddContact(User user)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.EmailId == user.EmailId);
            if( _user == null)
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user.Id;
            }
            else
            {
                return _userMessages.ContactExists;
            }
        }

        public async Task<object> AddUserAsAdmin(UserRole userRole)
        {
                await _dbContext.UserRoles.AddAsync(userRole);
                await _dbContext.SaveChangesAsync();
                return userRole;
        }

        public async Task<List<ContactInfoDto>> GetContacts(Guid companyId)
        {
            var users = await _dbContext.UserRoles
                .Where(x => x.CompanyId == companyId && x.ProductId == null)
                .ToListAsync();
            if(users == null)
            {
                return null;
            }
            var userGuids = users.Select(x=>x.UserId).ToList();
            var UsersRegisteredAsContacts = await _dbContext.Users
                .Where(x=>userGuids.Contains(x.Id) && !x.IsDeleted)
                .Select(x=> new ContactInfoDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    Designation = x.Designation
                })
                .ToListAsync();

            return UsersRegisteredAsContacts;
            
        }

        public async Task<object> DeleteContact(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.EmailId == email);
            if(user == null)
            {
                return _userMessages.UserNotExist;
            }
            user.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return _userMessages.ContactDeleted;
        }

        public async Task<object?> UpdateContact(string email, UpdateContactDto updatecontactDto)
        {
            var userToBeEdit = await _dbContext.Users.FirstOrDefaultAsync(x=>x.EmailId == email);
            if(userToBeEdit == null)
            {
                return null;
            }
            var userInUserRoleTable = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userToBeEdit.Id);
            if(updatecontactDto.IsAdmin)
            {
                userInUserRoleTable.RoleId = 3;
                await _dbContext.SaveChangesAsync();
            }
            _mapper.Map(updatecontactDto, userToBeEdit);
            await _dbContext.SaveChangesAsync();
            return _userMessages.ContactEdited;
        }

        public async Task<object?> AddProductContact(User user)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId);
            if(_user == null )
            {
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user.Id;
            }
            else
            {
                return _userMessages.ContactExists;
            }
           
        }

        public async Task<List<ContactInfoDto>> GetProductContact(Guid productId)
        {
            var contactId = await _dbContext.UserRoles
                .Where(x => x.ProductId == productId)
                .ToListAsync();

            if(contactId == null )
            {
                return null;
            }
            var userGuid = contactId.Select(x=>x.UserId).ToList();

            var contacts = await _dbContext.Users
                .Where(x => userGuid.Contains(x.Id) && !x.IsDeleted)
                .Select(x => new ContactInfoDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    Designation = x.Designation
                })
                .ToListAsync();
            return contacts;
        }
       
    }
}
