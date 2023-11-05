using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Host.Controllers;
using Xunit;

namespace UnitTesting
{
    public class ProductControllerTest
    {
        private readonly IProductService _companyBLL;
        [Fact]
        public async Task PostCompanyTest()
        {
            var productBLL = Substitute.For<IProductService>();
            var productdto = new ProductDto
            {
                Name = "Apple",
                Description = "A Mobile",
                Type = "Electronic Device",
                LogoImage = "64bitimage",
                ShortName = "Apple",
                //CreatedBy = Guid.Parse("524e7928-99e5-47c6-90c5-08dbb7fb271e"),
                //ModifiedBy = Guid.Parse("524e7928-99e5-47c6-90c5-08dbb7fb271e"),
            };

            var productcontroller = new ProductController(productBLL);
            var result = await productcontroller.PostProduct(productdto) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

       

    }
}
