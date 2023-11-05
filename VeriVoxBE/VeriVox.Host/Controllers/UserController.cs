using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Core.Messages;
using VeriVox.Database.DatabaseObjects;


namespace VeriVox.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserService _userService;
        private readonly UserMessages _userMessages;

        public UserController(IUserService userService, UserMessages userMessages)
        {

            _userService = userService;
            _userMessages = userMessages;
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult<object>> AddUser([FromBody] UserAddDto userAddDto)
        {
            var validate = new UserValidations();
            var validationResult = validate.ValidateAndGetErrors(userAddDto);

            if (validationResult != null)
            {
                return validationResult;
            }
            else
            {
                return await _userService.AddUser(userAddDto);
            }
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserGetDto>>> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteUser(string email)
        {
            return Ok(await _userService.DeleteUser(email));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserUpdateDto>> UpdateUser(Guid id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var updatedUser = await _userService.UpdateUser(id, userUpdateDto);
            return Ok(updatedUser);
        }

        [HttpPost("login")]
        public async Task<string> Login(LoginData loginData)
        {
            return await _userService.Login(loginData);
        }

        [Authorize(Policy = "AddContact")]
        [HttpPost("CreateContact")]
        public async Task<object?> AddContact(ContactDto contactDto)
        {
            return await _userService.AddContact(contactDto);
        }

        [Authorize(Policy ="ViewContacts")]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ContactInfoDto>>> GetContacts(Guid companyId, [FromQuery] int page=1, [FromQuery] int pageSize=3)
        {
            var contacts =  await _userService.GetContacts(companyId);
            
            int totalContacts = contacts.Count;
            var paginatedContacts = contacts
                .Skip((page-1)*pageSize).Take(pageSize).ToList();

            var paginatedResult = new PaginatedResult<ContactInfoDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalContacts,
                Data = paginatedContacts
            };
            return Ok(paginatedResult);

        }

        [HttpPut("deleteContact")]
        public async Task<object?> DeleteContact(string email)
        {
            return await _userService.DeleteContact(email);
        }

        [HttpPut("editContact")]
        public async Task<object?> UpdateContact(string email, UpdateContactDto updatecontactDto)
        {
            return await _userService.UpdateContact(email, updatecontactDto);
        }

        [HttpPost("createProductContact")]
        public async Task<object?> AddProductContact(ProductContactDto productContactDto)
        {
            return await _userService.AddProductContact(productContactDto);
        }

        [HttpGet("GetProductContact")]
        public async Task<ActionResult<PaginatedResult<ContactInfoDto>>> GetProductContact(Guid productId, 
            [FromQuery] int page=1, [FromQuery] int pageSize =3)
        {
            var contacts =  await _userService.GetProductContact(productId);
            int totalRecords = contacts.Count;
            var paginatedContacts = contacts
                .Skip((page-1) * pageSize).Take(pageSize).ToList();

            var paginatedResult = new PaginatedResult<ContactInfoDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = paginatedContacts
            };
            return Ok(paginatedResult);
        }

        [HttpGet("LoginByGoogle")]
        public async Task<string> GoogleLogin(string email)
        {
            return await _userService.GoogleLogin(email);
        }
    }
    
}
