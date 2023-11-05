using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeriVox.Business;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Host.Controllers;
using Xunit;

namespace UnitTesting
{
    public class LinksControllerTest
    {
        //[Fact]
        //public async Task PostLinks_Should_Return_OkResult()
        //{
        //    // Arrange
        //    var mockLinksBLL = new Mock<LinksBLL>();
        //    var linksController = new LinksController(mockLinksBLL.Object);
        //    var linksDto = new LinksDto();

        //    // Act
        //    var result = await linksController.PostLinks(linksDto);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public void GetLinks_Should_Return_OkResult()
        //{
        //    // Arrange
        //    var mockLinksBLL = new Mock<LinkService>();
        //    var linksController = new LinkController(mockLinksBLL.Object);
        //    var linkEntities = new List<Link>
        //    {
        //        // Your link entities here
        //    };

        //    mockLinksBLL.Setup(bll => bll.GetLinks()).Returns(linkEntities);

        //    // Act
        //    var result = linksController.GetLinks();

        //    // Assert
        //    var okResult = Assert.IsType<ActionResult<IEnumerable<LinkDto>>>(result);
        //    Assert.IsType<OkObjectResult>(okResult.Result);

        //    var linkDTOs = Assert.IsAssignableFrom<IEnumerable<LinkDto>>(okResult.Value);
        //    Assert.Equal(linkEntities.Count, linkDTOs.Count());
        //}

        //[Fact]
        //public void PutLink_Should_Return_OkResult()
        //{
        //    // Arrange
        //    var mockLinksBLL = new Mock<LinksBLL>();
        //    var linksController = new LinksController(mockLinksBLL.Object);
        //    var id = Guid.NewGuid();
        //    var linksDto = new LinksDto();

        //    // Act
        //    var result = linksController.PutLink(id, linksDto);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public void DeleteLink_Should_Return_OkResult()
        //{
        //    // Arrange
        //    var mockLinksBLL = new Mock<LinksBLL>();
        //    var linksController = new LinksController(mockLinksBLL.Object);
        //    var id = Guid.NewGuid();

        //    // Act
        //    var result = linksController.DeleteLink(id);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}
    }
}
