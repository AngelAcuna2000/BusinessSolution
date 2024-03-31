using LARemodeling.Controllers;
using LARemodeling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace LARemodeling.Tests;

public class HomeControllerTests
{
    private readonly Mock<ILARemodelingRepo> _mockRepo = new();

    [Fact]
    public void Index_ReturnsViewWithInquiryModel()
    {
        // Arrange
        var controller = new HomeController(_mockRepo.Object)
        {
            TempData = new TempDataDictionary(new DefaultHttpContext(), new Mock<ITempDataProvider>().Object)
        };

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.IsType<InquiryModel>(viewResult.Model);
    }

    // Other test methods (FormSubmit, InquiryManager) remain unchanged

    [Fact]
    public void Portfolio_ReturnsView()
    {
        // Arrange
        var controller = new HomeController(_mockRepo.Object);

        // Act
        var result = controller.Portfolio();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_ReturnsView()
    {
        // Arrange
        var controller = new HomeController(_mockRepo.Object);

        // Act
        var result = controller.Privacy();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_ReturnsErrorViewModel()
    {
        // Arrange
        var controller = new HomeController(_mockRepo.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { TraceIdentifier = "TraceId" }
            }
        };

        // Act
        var result = controller.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        var model = Assert.IsType<ErrorViewModel>(viewResult.Model);

        Assert.Equal("TraceId", model.RequestId);
    }
}
