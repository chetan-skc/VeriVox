//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VeriVox.Business.Interfaces;
//using VeriVox.Core.DataTransferObjects;
//using VeriVox.Host.Controllers;
//using Xunit;

//namespace UnitTesting
//{
//    public class FormControllerTest
//    {
//        [Fact]
//        public async Task GetAllForms_Should_Return_All_Forms()
//        {
//            //Arrange
//            var mockFormsService = new Mock<IFormService>();
//            var formsDto = new List<FormDto>
//            {
//                new FormDto
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Form 1",
//                    Description = "This is Form 1 description",
//                    NameOnFormURL  = "F1",
//                    ScopeId = 1,
//                    CreatedEntityId = Guid.NewGuid(),
//                    IsActive = true,
//                    IsDeleted = false,
//                    CreatedBy = Guid.NewGuid(),
//                    CreatedDate = DateTime.Now,
//                    ModifiedBy = Guid.NewGuid(),
//                    ModifiedDate = DateTime.Now,
//                 },
//                new FormDto
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Form 2",
//                    Description = "This is Form 2 description",
//                    NameOnFormURL  = "F2",
//                    ScopeId = 1,
//                    CreatedEntityId = Guid.NewGuid(),
//                    IsActive = true,
//                    IsDeleted = false,
//                    CreatedBy = Guid.NewGuid(),
//                    CreatedDate = DateTime.Now,
//                    ModifiedBy = Guid.NewGuid(),
//                    ModifiedDate = DateTime.Now,
//                 }
//            };

//            mockFormsService.Setup(service => service.GetAllAsync()).ReturnsAsync(formsDto);
//            var formController = new FormController(mockFormsService.Object);

//            //Act
//            var result = await formController.GetAllForms();

//            //Assert
//            Assert.IsType<ActionResult<List<FormDto>>>(result);
//        }

//        [Fact]
//        public async Task GetFormById_Should_Return_Form_When_Exists()
//        {
//            //Arrange
//            var mockFormsService = new Mock<IFormService>();
//            var existingFormsDto = new FormDto
//            {
//                Id = Guid.NewGuid(),
//                Name = "Form 3",
//                Description = "This is Form 3 description",
//                NameOnFormURL  = "F3",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,
//            };

//            mockFormsService.Setup(service => service.GetByIdAsync(existingFormsDto.Id)).ReturnsAsync(existingFormsDto);
//            var formController = new FormController(mockFormsService.Object);

//            //Act
//            var result = await formController.GetFormById(existingFormsDto.Id);

//            //Assert
//            Assert.IsType<ActionResult<FormDto>>(result);
//        }

//        [Fact]
//        public async Task GetFormById_Should_Return_NotFound_When_Form_Does_Not_Exist()
//        {
//            // Arrange
//            var mockFormService = new Mock<IFormService>();
//            var mockMapper = new Mock<IMapper>();
//            var formController = new FormController(mockFormService.Object);
//            var formId = Guid.NewGuid();
//            mockFormService.Setup(s => s.GetByIdAsync(formId)).ReturnsAsync((FormDto)null);

//            // Act
//            var result = await formController.GetFormById(formId);

//            // Assert
//            Assert.IsType<NotFoundObjectResult>(result.Result);
//        }


//        [Fact]
//        public async Task CreateForm_Should_Create_Form()
//        {
//            //Arrange
//            var mockFormService = new Mock<IFormService>();

//            var addFormsDto = new ModifyFormDto
//            {
//                Name = "Form 3",
//                Description = "This is Form 3 description",
//            };

//            var createdFormDto = new FormDto
//            {
//                Name = "Form 3",
//                Description = "This is Form 3 description",
//                NameOnFormURL  = "F3",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,
//            };

//            mockFormService.Setup(service => service.CreateAsync(addFormsDto)).ReturnsAsync(true);

//            var formController = new FormController(mockFormService.Object);

//            //Act
//            var result = await formController.CreateForm(addFormsDto);

//            //Assert
//            Assert.IsType<ActionResult<FormDto>>(result);


//        }

//        [Fact]
//        public async Task UpdateForm_Should_Return_OkObjectsResult()
//        {
//            //Arrange
//            var mockFormService = new Mock<IFormService>();
//            var mockMapper = new Mock<IMapper>();

//            var updateFormsDto = new ModifyFormDto
//            {
//                Name = "New Form",
//                Description = "This is new form description"
//            };

//            var updatedFormDto = new FormDto
//            {
//                Id = Guid.NewGuid(),
//                Name = "New Form",
//                Description = "This is new Form description",
//                NameOnFormURL  = "NF",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,
//            };

//            mockFormService.Setup(service => service.UpdateAsync(updatedFormDto.Id, updateFormsDto)).ReturnsAsync(true);
//            var formController = new FormController(mockFormService.Object);

//            //Act
//            var result = await formController.UpdateForm(updatedFormDto.Id, updateFormsDto);

//            //Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task UpdateForm_Should_Return_NotFound_When_Form_Does_Not_Found()
//        {
//            // Arrange
//            var mockFormService = new Mock<IFormService>();
//            var mockMapper = new Mock<IMapper>();
//            var formController = new FormController(mockFormService.Object);
//            var formId = Guid.NewGuid();
//            var updateFormDto = new ModifyFormDto();

//            mockFormService.Setup(r => r.UpdateAsync(formId, updateFormDto)).ReturnsAsync(false);

//            // Act
//            var result = await formController.UpdateForm(formId, updateFormDto);

//            // Assert
//            Assert.IsAssignableFrom<ActionResult<FormDto>>(result);
//        }


//        [Fact]
//        public async Task DeleteForm_Should_Delete_Form_When_Exists()
//        {
//            //Arrange
//            var mockFormService = new Mock<IFormService>();
//            var mockMapper = new Mock<IMapper>();

//            var formController = new FormController(mockFormService.Object);

//            var deletedFormDto = new FormDto
//            {
//                Id = Guid.NewGuid(),
//                Name = "Form 3",
//                Description = "This is Form 3 description",
//                NameOnFormURL  = "F3",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,
//            };

//            mockFormService.Setup(s => s.DeleteAsync(deletedFormDto.Id)).ReturnsAsync(true);

//            //Act
//            var result = await formController.DeleteForm(deletedFormDto.Id);

//            //Assert
//            var noContentResult = Assert.IsType<OkObjectResult>(result);
//        }

//        [Fact]
//        public async Task DeleteForm_Should_Return_NotFound_When_Form_Does_Not_Exist()
//        {
//            // Arrange
//            var mockFormService = new Mock<IFormService>();
//            var mockMapper = new Mock<IMapper>();
//            var formController = new FormController(mockFormService.Object);
//            var formId = Guid.NewGuid();
//            mockFormService.Setup(s => s.DeleteAsync(formId)).ReturnsAsync(false);

//            // Act
//            var result = await formController.DeleteForm(formId);

//            // Assert
//            Assert.IsAssignableFrom<ActionResult>(result);
//        }

//    }
//}
