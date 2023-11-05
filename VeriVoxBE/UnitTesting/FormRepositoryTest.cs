//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VeriVox.Database.Context;
//using VeriVox.Database.DatabaseObjects;
//using VeriVox.Repository;
//using Xunit;

//namespace UnitTesting
//{
//    public class FormRepositoryTest
//    {
//        [Fact]
//        public async Task GetAllAsync_Should_Return_All_Forms()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);

//                var formsList = new List<Form>
//                {
//                    new Form
//                    {
//                        Id = Guid.NewGuid(),
//                        Name = "Form 1",
//                        Description = "This is Form 1 description",
//                        NameOnFormURL  = "F1",
//                        ScopeId = 1,
//                        CreatedEntityId = Guid.NewGuid(),
//                        IsActive = true,
//                        IsDeleted = false,
//                        CreatedBy = Guid.NewGuid(),
//                        CreatedDate = DateTime.Now,
//                        ModifiedBy = Guid.NewGuid(),
//                        ModifiedDate = DateTime.Now,
//                    },
//                    new Form
//                    {
//                        Id = Guid.NewGuid(),
//                        Name = "Form 2",
//                        Description = "This is Form 2 description",
//                        NameOnFormURL  = "F2",
//                        ScopeId = 1,
//                        CreatedEntityId = Guid.NewGuid(),
//                        IsActive = true,
//                        IsDeleted = false,
//                        CreatedBy = Guid.NewGuid(),
//                        CreatedDate = DateTime.Now,
//                        ModifiedBy = Guid.NewGuid(),
//                        ModifiedDate = DateTime.Now,
//                    }
//                };

//                // Seeding the in-memory database with sample data
//                dbContext.Form.AddRange(formsList);
//                dbContext.SaveChanges();

//                // Act
//                var result = await formsRepository.GetAllAsync();

//                // Assert
//                Assert.NotNull(result);
//                Assert.Equal(2, result.Count);
//            }
//        }

//        [Fact]
//        public async Task GetByIdAsync_Should_Return_Form_When_Exists()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);

//                var formId = Guid.NewGuid();
//                var form = new Form
//                {
//                    Id = formId,
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
//                };

//                dbContext.Form.Add(form);
//                dbContext.SaveChanges();

//                // Act
//                var result = await formsRepository.GetByIdAsync(formId);

//                // Assert
//                Assert.NotNull(result);
//                Assert.Equal(formId, result.Id);
//            }
//        }

//        [Fact]
//        public async Task GetByIdAsync_Should_Return_Null_When_Form_Does_Not_Exist()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);
//                var nonExistentFormId = Guid.NewGuid();

//                // Act
//                var result = await formsRepository.GetByIdAsync(nonExistentFormId);

//                // Assert
//                Assert.Null(result);
//            }
//        }

//        [Fact]
//        public async Task CreateAsync_Should_Create_Form()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);

//                var addForm = new Form
//                {
//                    Name = "Form 3",
//                    Description = "This is Form 3 description",
//                    ScopeId = 1,
//                    CreatedEntityId = Guid.NewGuid(),
//                    IsActive = true,
//                    IsDeleted = false,
//                    CreatedBy = Guid.NewGuid(),
//                    ModifiedBy = Guid.NewGuid()
//                };


                

//                // Act
//                var createdFormEntity = await formsRepository.CreateAsync(addForm);

//                // Assert
//                Assert.NotNull(createdFormEntity);
//                Assert.True(createdFormEntity);
//            }
//        }


//        [Fact]
//        public async Task UpdateAsync_Should_Update_Form_When_Exists()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);

//                var formId = Guid.NewGuid();
//                var existingForm = new Form
//                {
//                    Id = formId,
//                    Name = "Original Title",
//                    Description = "Original Description",
//                    NameOnFormURL  = "OT",
//                    ScopeId = 1,
//                    CreatedEntityId = Guid.NewGuid(),
//                    IsActive = true,
//                    IsDeleted = false,
//                    CreatedBy = Guid.NewGuid(),
//                    CreatedDate = DateTime.Now,
//                    ModifiedBy = Guid.NewGuid(),
//                    ModifiedDate = DateTime.Now,
//                };

//                dbContext.Form.Add(existingForm);
//                dbContext.SaveChanges();

//                var updateForm = new Form
//                {
//                    Id = formId,
//                    Name = "Updated Title",
//                    Description = "Updated Description",
//                    NameOnFormURL  = "UT",
//                    ScopeId = 1,
//                    CreatedEntityId = Guid.NewGuid(),
//                    IsActive = true,
//                    IsDeleted = false,
//                    CreatedBy = Guid.NewGuid(),
//                    CreatedDate = DateTime.Now,
//                    ModifiedBy = Guid.NewGuid(),
//                    ModifiedDate = DateTime.Now,
//                };

//                // Act
//                var result = await formsRepository.UpdateAsync(formId, updateForm);

//                // Assert
//                Assert.NotNull(result);
//                Assert.Equal("Updated Title", result.Name);
//                Assert.Equal("Updated Description", result.Description);
//            }
//        }

//        [Fact]
//        public async Task UpdateAsync_Should_Return_Null_When_Form_Does_Not_Exist()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);
//                var nonExistentFormId = Guid.NewGuid();
//                var updateForm = new Form();

//                // Act
//                var result = await formsRepository.UpdateAsync(nonExistentFormId, updateForm);

//                // Assert
//                Assert.Null(result);
//            }
//        }

//        [Fact]
//        public async Task DeleteAsync_Should_Delete_Form_When_Exists()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);

//                var formId = Guid.NewGuid();
//                var existingForm = new Form
//                {
//                    Id = formId,
//                    Name = "Form A",
//                    Description = "This is Form A Description",
//                    NameOnFormURL  = "FA",
//                    ScopeId = 1,
//                    CreatedEntityId = Guid.NewGuid(),
//                    IsActive = true,
//                    IsDeleted = false,
//                    CreatedBy = Guid.NewGuid(),
//                    CreatedDate = DateTime.Now,
//                    ModifiedBy = Guid.NewGuid(),
//                    ModifiedDate = DateTime.Now,
//                };

//                dbContext.Form.Add(existingForm);
//                dbContext.SaveChanges();

//                // Act
//                var result = await formsRepository.DeleteAsync(formId);

//                // Assert
//                Assert.NotNull(result);
//            }
//        }

//        [Fact]
//        public async Task DeleteAsync_Should_Return_Null_When_Form_Does_Not_Exist()
//        {
//            // Arrange
//            var dbContextOptions = new DbContextOptionsBuilder<CFA_DbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            using (var dbContext = new CFA_DbContext(dbContextOptions))
//            {
//                var formsRepository = new FormRepository(dbContext);
//                var nonExistentFormId = Guid.NewGuid();

//                // Act
//                var result = await formsRepository.DeleteAsync(nonExistentFormId);

//                // Assert
//                Assert.False(result);
//            }
//        }
//    }
//}
