//using AutoMapper;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VeriVox.Business;
//using VeriVox.Core.DataTransferObjects;
//using VeriVox.Database.DatabaseObjects;
//using VeriVox.Repository.Interfaces;
//using Xunit;

//namespace UnitTesting
//{
//    public class FormServiceTest
//    {
//        [Fact]
//        public async Task GetAllAsync_Shoul_Return_All_Forms()
//        {
//            // Arrange
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();


//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );
//            var formsDomainList = new List<Form>
//            {
//                new Form
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
//                new Form
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

//            //Configure the repository mock to return data
//            mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(formsDomainList);

//            //Using AutoMapper configuration for mapping
//            mockMapper.Setup(m => m.Map<List<FormDto>>(formsDomainList))
//                .Returns(formsDomainList.Select(f => new FormDto
//                {
//                    Id = f.Id,
//                    Name = f.Name,
//                    Description = f.Description,
//                    NameOnFormURL  = f.NameOnFormURL ,
//                    ScopeId = f.ScopeId,
//                    CreatedEntityId = f.CreatedEntityId,
//                    IsActive = f.IsActive,
//                    IsDeleted = f.IsDeleted,
//                    CreatedBy = f.CreatedBy,
//                    CreatedDate = f.CreatedDate,
//                    ModifiedBy = f.ModifiedBy,
//                    ModifiedDate = f.ModifiedDate,
//                }).ToList());

//            //Act
//            var result = await formService.GetAllAsync();

//            //Assert
//            Assert.NotNull(result);
//            Assert.Equal(2, result.Count);
//        }

//        [Fact]
//        public async Task GetByIdAsync_Should_Return_Form_When_Exists()
//        {
//            //Arrange
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();


//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );

//            var formId = Guid.NewGuid();
//            var formDomain = new Form
//            {
//                Id = formId,
//                Name = "Form 1",
//                Description = "This is Form 1 description",
//                NameOnFormURL  = "F1",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,
//            };
//            var formDto = new FormDto
//            {
//                Id = formId,
//                Name = "Form 1",
//                Description = "This is Form 1 description",
//                NameOnFormURL  = "F1",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,
//            };

//            mockRepository.Setup(r => r.GetByIdAsync(formId)).ReturnsAsync(formDomain);
//            mockMapper.Setup(m => m.Map<FormDto>(formDomain)).Returns(formDto);

//            //Act
//            var result = await formService.GetByIdAsync(formId);

//            //Assert
//            Assert.NotNull(result);
//            Assert.Equal(formId, result.Id);
//        }

//        [Fact]
//        public async Task GetByIdAsync_Should_Return_Null_When_Form_Does_Not_Exist()
//        {
//            //Arrange
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();


//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );

//            var formId = Guid.NewGuid();

//            mockRepository.Setup(r => r.GetByIdAsync(formId)).ReturnsAsync((Form)null);

//            //Act
//            var result = await formService.GetByIdAsync(formId);

//            //Assert
//            Assert.Null(result);
//        }

//        [Fact]
//        public async Task CreateAsync_Should_Create_Form()
//        {
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();


//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );

//            var addFormDto = new ModifyFormDto
//            {
//                Name = "Form 3",
//                Description = "This is Form 3 description"
//            };

//            var createdForm = new Form
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
//                ModifiedDate = DateTime.Now
//            };

//            var createdFormDto = new FormDto
//            {
//                Id = createdForm.Id,
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

//            mockMapper.Setup(m => m.Map<Form>(It.IsAny<ModifyFormDto>())).Returns(createdForm);
//            mockRepository.Setup(r => r.CreateAsync(It.IsAny<Form>())).ReturnsAsync(true);
//            mockMapper.Setup(m => m.Map<FormDto>(It.IsAny<Form>())).Returns(createdFormDto);


//            //Act
//            var result = await formService.CreateAsync(addFormDto);

//            // Assert
//            Assert.True(result);

//        }

//        [Fact]
//        public async Task UpdateAsync_Should_Update_Form_When_Exists()
//        {
//            //Arrange
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();


//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );

//            var formId = Guid.NewGuid();
//            var updateFormDto = new ModifyFormDto
//            {
//                Name = "Updated Title",
//                Description = "Updated Description"
//            };

//            var existingForm = new Form
//            {
//                Id = formId,
//                Name = "Original Title",
//                Description = "Original Description",
//                NameOnFormURL  = "OT",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,
//            };

//            var updatedFormDto = new FormDto
//            {
//                Id = formId,
//                Name = "Updated Title",
//                Description = "Updated Description",
//                NameOnFormURL  = "UT",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,

//            };

//            mockMapper.Setup(m => m.Map<Form>(updatedFormDto)).Returns(existingForm);
//            mockRepository.Setup(r => r.UpdateAsync(formId, It.IsAny<Form>())).ReturnsAsync(existingForm);
//            mockMapper.Setup(m => m.Map<FormDto>(existingForm)).Returns(updatedFormDto);

//            //Act
//            var result = await formService.UpdateAsync(formId, updateFormDto);

//            //Assert
//            Assert.True(result);


//        }


//        [Fact]
//        public async Task UpdatedAsync_Should_Return_Null_When_Form_Does_Not_Exists()
//        {
//            // Arrange
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();

//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );

//            var formId = Guid.NewGuid();
//            var updateFormDto = new ModifyFormDto
//            {
//                Name = "Updated Title",
//                Description = "Updated Description"
//            };

//            // Configure the repository mock to return null when UpdateAsync is called
//            mockRepository.Setup(r => r.UpdateAsync(formId, It.IsAny<Form>())).ReturnsAsync((Form)null);

//            // Act and Assert
//            var ex = await Assert.ThrowsAsync<ApplicationException>(async () =>
//            {
//                await formService.UpdateAsync(formId, updateFormDto);
//            });

//            Assert.Equal("Form not found.", ex.Message);

//        }

//        [Fact]
//        public async Task DeleteAsync_Should_Delete_Form_When_Exists()
//        {
//            //Arrange
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();


//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );

//            var formId = Guid.NewGuid();

//            var existingForm = new Form
//            {
//                Id = formId,
//                Name = "Form A",
//                Description = "This is Form A Description",
//                NameOnFormURL  = "FA",
//                ScopeId = 1,
//                CreatedEntityId = Guid.NewGuid(),
//                IsActive = true,
//                IsDeleted = false,
//                CreatedBy = Guid.NewGuid(),
//                CreatedDate = DateTime.Now,
//                ModifiedBy = Guid.NewGuid(),
//                ModifiedDate = DateTime.Now,

//            };

//            mockRepository.Setup(r => r.DeleteAsync(formId)).ReturnsAsync(true);
//            mockMapper.Setup(m => m.Map<FormDto>(It.IsAny<Form>())).Returns(new FormDto());

//            //Act
//            var result = await formService.DeleteAsync(formId);

//            //Assert
//            Assert.NotNull(result);
//        }

//        [Fact]
//        public async Task DeleteAsync_Should_Return_Null_When_Form_Does_Not_Exist()
//        {
//            // Arrange
//            var mockRepository = new Mock<IFormRepository>();
//            var mockMapper = new Mock<IMapper>();

//            var formService = new FormService(
//                mockRepository.Object,
//                mockMapper.Object
//            );

//            var formId = Guid.NewGuid();

//            // Configuring the repository mock to return false when DeleteAsync is called
//            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

//            // Act and Assert
//            var ex = await Assert.ThrowsAsync<ApplicationException>(async () =>
//            {
//                await formService.DeleteAsync(formId);
//            });

//            Assert.Equal("Form not found.", ex.Message);

//        }
//    }
//}
