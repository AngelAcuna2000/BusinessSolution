using InquiryApp.Models;

namespace InquiryApp.Tests;

public class ErrorViewModelTests
{
    [Fact]
    public void ShowRequestId_IsFalse_WhenRequestIdIsNull()
    {
        // Arrange
        var model = new ErrorViewModel { RequestId = null };

        // Act
        var showRequestId = model.ShowRequestId;

        // Assert
        Assert.False(showRequestId);
    }

    [Fact]
    public void ShowRequestId_IsFalse_WhenRequestIdIsEmpty()
    {
        // Arrange
        var model = new ErrorViewModel { RequestId = string.Empty };

        // Act
        var showRequestId = model.ShowRequestId;

        // Assert
        Assert.False(showRequestId);
    }

    [Fact]
    public void ShowRequestId_IsTrue_WhenRequestIdIsNotEmpty()
    {
        // Arrange
        var model = new ErrorViewModel { RequestId = "some-request-id" };

        // Act
        var showRequestId = model.ShowRequestId;

        // Assert
        Assert.True(showRequestId);
    }
}
