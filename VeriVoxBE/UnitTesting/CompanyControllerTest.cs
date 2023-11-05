using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Host.Controllers;
using VeriVox.Repository.Access_DB;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;

namespace UnitTesting
{
    public class CompanyControllerTest
    {

        /*
        private readonly Mock<ICompanyDal> _companyDal;
        private readonly ICompanyBll _companyBll;

        public CompanyControllerTest()
        {
            _companyDal = new Mock<ICompanyDal>();
            _companyBll = new CompanyBll(_companyDal.Object);
        }

        private List<CompanyDto> company = new List<CompanyDto> { 
            new CompanyDto
                { Name = "Apple",
                Description = "A Mobile hardware company",
                LogoImage = "64bitimage",
                ShortName = "Google",
                IndustryId = Guid.Parse("597933cf-b59a-4c3c-8536-4fc480c72853"),
                CreatedBy = Guid.Parse("0ec32f02-49ef-4d0f-7f54-08dbb291745e"),
                ModifiedBy = Guid.Parse("0ec32f02-49ef-4d0f-7f54-08dbb291745e") 
                }  };

        */


  

        [Fact]
        public async Task PostCompanyTest()
        {
            var companyBLL = Substitute.For<ICompanyService>();
            var companydto = new CompanyDto
            {
                Name = "Apple",
                Description = "A Mobile hardware company",
                LogoImage = "64bitimage",
                ShortName = "Google",
                IndustryId = Guid.Parse("597933cf-b59a-4c3c-8536-4fc480c72853"),

            };

            var companycontroller = new CompanyController(companyBLL);
            var result = await companycontroller.PostCompany(companydto) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);

        }






    }
}
