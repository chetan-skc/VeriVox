using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Core.Messages;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;

namespace VeriVox.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserMessages _userMessages;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IUserRepository iuserRepository, IMapper mapper, IConfiguration configuration, UserMessages userMessages, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = iuserRepository;
            _mapper = mapper;
            _configuration = configuration;
            _userMessages = userMessages;
            _contextAccessor = httpContextAccessor;
        }

        public static string HashPassword(string password, IConfiguration _configuration)
        {
            string? passphrase = _configuration["AppSettings:Passphrase"];
            byte[] secretkey = Encoding.UTF8.GetBytes(passphrase);
            string Password;
            using (var hmac = new HMACSHA512(secretkey))
            {
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                Password = $"{hash}";
            }

            return Password;
        }

        public async Task<List<UserGetDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var userDtos = users.Select(user => new UserGetDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailId = user.EmailId
            }).ToList();

            return userDtos;
        }

        public async Task<object?> DeleteUser(string email)
        {
            var user = await _userRepository.DeleteUser(email);
            if(user == null)
            {
                return _userMessages.UserNotExist;
            }
            else
            {
                var userDto = _mapper.Map<User, UserGetDto>(user);
                return _userMessages.UserDeleted;
            } 
        }

        private bool VerifyPassword(string email, string password, string storedEmail, string storedPassword)
        {
            string ?passphrase = _configuration["AppSettings:Passphrase"];
            byte[] secretkey = Encoding.UTF8.GetBytes(passphrase);
            using (var hmac = new HMACSHA512(secretkey))
            {
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                password = $"{hash}";
            }

            

            if (password == storedPassword && email == storedEmail)
            {
                
                return true;
            }
            return false;
        }

        public async Task<string> Login(LoginData loginData)
        {

            var user = await _userRepository.GetUserByEmail(loginData.Email);
            if (user == null)
            {
                return _userMessages.InvalidCredentials;
            }


            var storedEmail = user.EmailId;
            var storedPassword = user.Password;

            if (!VerifyPassword(loginData.Email, loginData.Password, storedEmail, storedPassword))
            {
                return _userMessages.InvalidCredentials;
            }

            var TokenGenerate = new TokenService(_userRepository, _configuration);
            string jwtToken = TokenGenerate.GenerateJwtToken(user);
            return jwtToken;
        }

        public async Task<object> AddUser(UserAddDto userAddDto)
        {
            var user = _mapper.Map<UserAddDto,User>(userAddDto);
            user.Password = HashPassword(user.Password, _configuration);
            var addUser = await _userRepository.AddContact(user);
            
            if(addUser != null)
            {
                return _userMessages.UserRegisterSuccess;   
            }
            else
            {
                return _userMessages.UserExists;
            }
            
        }

        public async Task<object?> UpdateUser(Guid id, UserUpdateDto userUpdateDto)
        {
            var user = _mapper.Map<UserUpdateDto, User>(userUpdateDto);

            user.Password = HashPassword(user.Password, _configuration);

            var updatedUser = await _userRepository.UpdateUser(id, user);

            return updatedUser;
        }
        
        public async Task<object?> AddContact(ContactDto contactDto)
        {
            var user = new User
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                EmailId = contactDto.EmailId,
                Designation = contactDto.Designation,
                CreatedBy = Guid.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value),
               
            };
            var ContactAdded = await _userRepository.AddContact(user);
            if (ContactAdded == _userMessages.ContactExists)
            {
                return ContactAdded;
            }
            var userGuid = ContactAdded;

            if (contactDto.IsAdmin)
            {
                var userRole = new UserRole
                {
                    UserId = (Guid)userGuid,
                    RoleId = (int)3,
                    CompanyId = contactDto.CompanyId,
                    CreatedBy = Guid.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value),


                };
                var AddingtoUserRole = await _userRepository.AddUserAsAdmin(userRole);
               
            }
            else 
            {
                var userRole = new UserRole
                {
                    UserId = (Guid)userGuid,
                    RoleId = (int)4,
                    CompanyId = contactDto.CompanyId,
                    CreatedBy = Guid.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value),


                };
                var AddingtoUserRole = await _userRepository.AddUserAsAdmin(userRole);
                
            }
            
           
            return _userMessages.ContactAdded;
           
            
        }

        public async Task<List<ContactInfoDto>> GetContacts(Guid companyId)
        {
            var users = await _userRepository.GetContacts(companyId);
            return users;
        }

        public async Task<object?> DeleteContact(string email)
        {
            return await _userRepository.DeleteContact(email);
        }

        public async Task<object?> UpdateContact(string email, UpdateContactDto updatecontactDto)
        {
            return await _userRepository.UpdateContact(email, updatecontactDto);
        }

        public async Task<object?> AddProductContact(ProductContactDto productContactDto)
        {
            var user = new User
            {
                FirstName = productContactDto.FirstName,
                LastName = productContactDto.LastName,
                EmailId = productContactDto.EmailId,
                Designation = productContactDto.Designation,
                CreatedBy = Guid.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value),
            };

            var contactAdded = await _userRepository.AddProductContact(user);
            if(contactAdded == _userMessages.ContactExists)
            {
                return contactAdded;
            }
            var userGuid = contactAdded;
            if (productContactDto.IsAdmin)
            {
                var userRole = new UserRole
                {
                    UserId = (Guid)userGuid,
                    RoleId = (int)5,
                    CompanyId = productContactDto.CompanyId,
                    ProductId = productContactDto.ProductId,
                    CreatedBy = Guid.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value),
                };
                var AddingtoUserRole = await _userRepository.AddUserAsAdmin(userRole);
            }
            else
            {
                var userRole = new UserRole
                {
                    UserId = (Guid)userGuid,
                    RoleId = (int)6,
                    CompanyId = productContactDto.CompanyId,
                    ProductId = productContactDto.ProductId,
                    CreatedBy = Guid.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value),


                };
                var AddingtoUserRole = await _userRepository.AddUserAsAdmin(userRole);

            }

            return _userMessages.ContactAdded;
        }

        public async Task<List<ContactInfoDto>> GetProductContact(Guid productId)
        {
            var contacts = await _userRepository.GetProductContact(productId);
            return contacts;
        }

        public async Task<string> GoogleLogin(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return _userMessages.InvalidCredentials;
            }
            var TokenGenerate = new TokenService(_userRepository, _configuration);
            string jwtToken = TokenGenerate.GenerateJwtToken(user);
            return jwtToken;
        }
    }
}
