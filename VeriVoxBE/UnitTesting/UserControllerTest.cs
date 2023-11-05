using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Serialization;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Core.Messages;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Host.Controllers;
using Xunit;

namespace UnitTesting
{
    public class UserControllerTest
    {
       

        [Fact]
        public async Task PostUser_ReturnsCreatedAtAction()
        {

            var mockIUserBLL = new Mock<IUserService>();
            var userAddDto = new UserAddDto
            {
                FirstName = "Rajendra",
                LastName = "Patel",
                EmailId = "rajendra.patel@qburst.com",
                Password = "Rajendra@123"
            };
            var userDto = new UserGetDto
            {
               
                FirstName = "Rajendra",
                LastName = "Patel",
                EmailId = "rajendra.patel@qburst.com",
                

            };

            mockIUserBLL.Setup(service => service.AddUser(userAddDto)).ReturnsAsync(userDto);
            var userController = new UserController(mockIUserBLL.Object,new UserMessages());
            //Act
            var result = await userController.AddUser(userAddDto);
            if (result.Result != null)
            {
                Assert.IsType<CreatedAtActionResult>(result.Result);
            }
            else
            {
                Console.WriteLine("AddUser returned null.");
            }


        }
        [Fact]
        public async Task GetUsers_Test()
        {
            var mockIUserBLL = new Mock<IUserService>();
            var users = new List<UserGetDto>
            {
                new UserGetDto
                {
                   FirstName = "Rajendra",
                   LastName = "Patel",
                   EmailId = "rajendra.patel@qburst.com",

                }
            };
            mockIUserBLL.Setup(service => service.GetUsers()).ReturnsAsync(users);
            var userController = new UserController(mockIUserBLL.Object,new UserMessages());

            var result = await userController.GetUsers();
            Assert.IsType<ActionResult<List<UserGetDto>>>(result);
        }

        [Fact]
        public async Task DeleteUser_Test()
        {
            var mockIUserBLL = new Mock<IUserService>();
            var userController = new UserController(mockIUserBLL.Object, new UserMessages());

            var deleteUserDto = new UserGetDto
            {
                FirstName = "Rajendra",
                LastName = "Patel",
                EmailId = "rajendra.patel@qburst.com"

            };
            mockIUserBLL.Setup(s => s.DeleteUser(deleteUserDto.EmailId)).ReturnsAsync(deleteUserDto);
            var result = await userController.DeleteUser(deleteUserDto.EmailId);
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

    }
}
