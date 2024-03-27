using BusinessSolutionShared;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;

namespace InquiryApp.Tests;

public class InquiryAppRepositoryTests
{
    private readonly Mock<IDapperWrapper> _mockDapperWrapper = new();
    private readonly Mock<IDbConnection> _mockConn = new();
    private readonly Mock<ILogger<InquiryAppRepository>> _mockLogger = new();
    private readonly InquiryAppRepository _repository;

    public InquiryAppRepositoryTests()
    {
        _repository = new InquiryAppRepository(_mockDapperWrapper.Object, _mockLogger.Object, _mockConn.Object);
    }

    [Fact]
    public void GetAllInquiries_SuccessReturnsAllInquiries()
    {
        // Arrange
        var expectedInquiries = new List<InquiryModel>
        {
            new() { Inquiry_ID = 1 },
            new() { Inquiry_ID = 2 }
        };

        _mockDapperWrapper.Setup(d => d.Query<InquiryModel>(
            _mockConn.Object, "SELECT * FROM inquiries;", null, null, true, null, null)).Returns(expectedInquiries);

        // Act
        var inquiries = _repository.GetAllInquiries();

        // Assert
        Assert.Equal(expectedInquiries, inquiries);
    }

    [Fact]
    public void GetAllInquiries_FailureReturnsEmptyCollection()
    {
        // Arrange
        _mockDapperWrapper.Setup(d => d.Query<InquiryModel>(
            _mockConn.Object, "SELECT * FROM inquiries;", null, null, true, null, null)).Throws(new Exception());

        // Act
        var inquiries = _repository.GetAllInquiries();

        // Assert
        Assert.Empty(inquiries);
    }

    [Fact]
    public void DeleteInquiry_SuccessReturnsTrue()
    {
        // Arrange
        var inquiryToDelete = new InquiryModel { Inquiry_ID = 1 };

        _mockDapperWrapper.Setup(d => d.Execute(
            _mockConn.Object,
            "DELETE FROM inquiries WHERE inquiry_id = @Inquiry_ID;",
            new { Inquiry_ID = 1 },
            null,
            null,
            null)).Returns(1);

        // Act
        var result = _repository.DeleteInquiry(inquiryToDelete);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteInquiry_FailureReturnsFalse()
    {
        // Arrange
        var inquiryToDelete = new InquiryModel { Inquiry_ID = 1 };

        _mockDapperWrapper.Setup(d => d.Execute(
            _mockConn.Object,
            It.IsAny<string>(),
            It.IsAny<object>(),
            It.IsAny<IDbTransaction>(),
            It.IsAny<int?>(),
            It.IsAny<CommandType?>())).Throws(new Exception());

        // Act
        var result = _repository.DeleteInquiry(inquiryToDelete);

        // Assert
        Assert.False(result);
    }
}
