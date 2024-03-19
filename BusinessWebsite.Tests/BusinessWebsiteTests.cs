using BusinessSolutionShared;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;

namespace BusinessWebsite.Tests;

public class BusinessWebsiteTests
{
    private readonly Mock<IDapperWrapper> _mockDapperWrapper;

    private readonly Mock<IDbConnection> _mockConn;

    private readonly ILogger<BusinessWebsiteRepository> _logger;

    private readonly BusinessWebsiteRepository _repository;

    public BusinessWebsiteTests()
    {
        _mockDapperWrapper = new Mock<IDapperWrapper>();

        _mockConn = new Mock<IDbConnection>();

        _logger = new Mock<ILogger<BusinessWebsiteRepository>>().Object;

        _mockDapperWrapper.Setup(d => d.Execute(It.IsAny<IDbConnection>(),

            It.IsAny<string>(),

            It.IsAny<object>(),

            null,

            null,

            null)).Returns(1);

        _repository = new BusinessWebsiteRepository(_mockDapperWrapper.Object, _logger, _mockConn.Object);
    }

    [Fact]
    public void InsertInquiry_SuccessReturnsTrue()
    {
        // Arrange
        var inquiry = new InquiryModel
        {
            Name = "Test",

            Phone = "1234567890", 

            Email = "test@example.com" 
        };

        // Act
        var result = _repository.InsertInquiry(inquiry);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void InsertInquiry_FailureReturnsFalse()
    {
        // Arrange
        var inquiry = new InquiryModel
        {
            Name = "Test",

            Phone = "1234567890", 

            Email = "test@example.com" 
        };

        var exception = new Exception("Database error");

        _mockDapperWrapper.Setup(d => d.Execute(It.IsAny<IDbConnection>(),

            It.IsAny<string>(),

            It.IsAny<object>(),

            null,

            null,

            null)).Throws(exception);

        // Act
        var result = _repository.InsertInquiry(inquiry);

        // Assert
        Assert.False(result);
    }
}
