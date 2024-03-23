using BusinessSolutionShared;
using BusinessWebsite.Controllers;
using BusinessWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Diagnostics;

namespace BusinessWebsite.Tests;

public class HomeControllerTests
{
    private readonly HomeController _controller;

    private readonly Mock<IBusinessWebsiteRepository> _mockRepo = new();

    private readonly Mock<IUrlHelper> _mockUrlHelper = new();

    private readonly TempDataDictionary _tempData;

    public HomeControllerTests()
    {
        _tempData = GetTempData();

        _controller = CreateController(_mockRepo.Object, _tempData);

        SetupUrlHelper(_mockUrlHelper, _controller);
    }

    private static TempDataDictionary GetTempData()
    {
        var tempDataProvider = new Mock<ITempDataProvider>();

        var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider.Object);

        return (TempDataDictionary)tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
    }

    private static HomeController CreateController(IBusinessWebsiteRepository repo, ITempDataDictionary tempData)
    {
        var controller = new HomeController(repo)
        {
            TempData = tempData,
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { TraceIdentifier = "TestTraceIdentifier" }
            }
        };
        controller.ControllerContext.HttpContext.RequestServices = new Mock<IServiceProvider>().Object;

        return controller;
    }

    private static void SetupUrlHelper(Mock<IUrlHelper> mockUrlHelper, Controller controller)
    {
        mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("/Home/Index#Contact-Us");

        controller.Url = mockUrlHelper.Object;
    }

    [Fact]
    public void Index_ReturnsViewWithInquiryModel()
    {
        // Act
        var result = _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.IsAssignableFrom<InquiryModel>(viewResult.Model);
    }

    [Fact]
    public void FormSubmit_InvalidModel_ReturnsRedirect()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Model is invalid");

        var inquiryModel = new InquiryModel();

        _mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("/Home/Index");

        // Act
        var result = _controller.FormSubmit(inquiryModel);

        // Assert
        var redirectResult = Assert.IsType<LocalRedirectResult>(result);

        Assert.Equal("/Home/Index#Contact-Us", redirectResult.Url);
    }

    [Fact]
    public void FormSubmit_ValidModel_ReturnsRedirectWithSuccessMessage()
    {
        // Arrange
        var inquiryModel = new InquiryModel { Name = "Test", Email = "test@example.com", Phone = "123-456-7890" };

        _mockRepo.Setup(repo => repo.InsertInquiry(inquiryModel)).Returns(true);

        _mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("/Home/Index");

        // Act
        var result = _controller.FormSubmit(inquiryModel);

        // Assert
        var redirectResult = Assert.IsType<LocalRedirectResult>(result);

        Assert.Equal("/Home/Index#Contact-Us", redirectResult.Url);

        Assert.Equal("Your inquiry has been sent.", _controller.TempData["Message"]);
    }

    [Fact]
    public void Portfolio_ReturnsView()
    {
        // Act
        var result = _controller.Portfolio();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_ReturnsView()
    {
        // Act
        var result = _controller.Privacy();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_ReturnsViewWithErrorViewModel()
    {
        // Arrange
        Activity.Current = new Activity("TestActivity");

        // Act
        var result = _controller.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.Model);

        Assert.Equal("TestTraceIdentifier", model.RequestId);
    }
}
