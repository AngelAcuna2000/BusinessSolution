using BusinessSolutionShared;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;

namespace BusinessWebsite.Tests;

public class BusinessWebsiteRepositoryTests
{
    private readonly Mock<IDapperWrapper> _mockDapperWrapper = new();

    private readonly Mock<IDbConnection> _mockConn = new();

    private readonly BusinessWebsiteRepository _repository;

    public BusinessWebsiteRepositoryTests()
    {
        _mockDapperWrapper.Setup(dapperWrapper => dapperWrapper.Execute(
            It.IsAny<IDbConnection>(),
            It.IsAny<string>(),
            It.IsAny<object>(),
            null,
            null,
            null)).Returns(1);

        var mockLogger = Mock.Of<ILogger<BusinessWebsiteRepository>>();

        _repository = new BusinessWebsiteRepository(_mockDapperWrapper.Object, mockLogger, _mockConn.Object);
    }

    [Fact]
    public void InsertInquiry_SuccessReturnsTrue()
    {
        // Arrange
        var inquiry = new InquiryModel { Name = "Test", Phone = "123-456-7890", Email = "test@example.com" };

        // Act
        var result = _repository.InsertInquiry(inquiry);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void InsertInquiry_FailureReturnsFalse()
    {
        // Arrange
        var inquiry = new InquiryModel { Name = "Test", Phone = "123-456-7890", Email = "test@example.com" };

        _mockDapperWrapper.Setup(dapperWrapper => dapperWrapper.Execute(
            It.IsAny<IDbConnection>(),
            It.IsAny<string>(),
            It.IsAny<object>(),
            null,
            null,
            null)).Throws(new Exception("Database error"));

        // Act
        var result = _repository.InsertInquiry(inquiry);

        // Assert
        Assert.False(result);
    }
}
