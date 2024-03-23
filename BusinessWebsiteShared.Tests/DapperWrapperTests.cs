using BusinessSolutionShared;
using Dapper;
using Moq;
using System.Data;

namespace BusinessWebsiteShared.Tests;

public class DapperWrapperTests
{
    private readonly Mock<IDbConnection> _mockConn = new();

    private readonly DapperWrapper _dapperWrapper = new();

    [Fact]
    public void Execute_ReturnsExpectedResult()
    {
        // Arrange
        var sql = "INSERT INTO table (column) VALUES (@value);";

        var parameters = new { value = "test" };

        _mockConn.Setup(conn => conn.Execute(sql, parameters, null, null, null)).Returns(1);

        // Act
        var result = _dapperWrapper.Execute(_mockConn.Object, sql, parameters);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void Query_ReturnsExpectedSequence()
    {
        // Arrange
        var sql = "SELECT * FROM table WHERE column = @value;";

        var parameters = new { value = "test" };

        var expected = new[] { new { Column = "test" } };

        _mockConn.Setup(conn => conn.Query<dynamic>(sql, parameters, null, true, null, null)).Returns(expected);

        // Act
        var result = _dapperWrapper.Query<dynamic>(_mockConn.Object, sql, parameters);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void QuerySingle_ReturnsExpectedObject()
    {
        // Arrange
        var sql = "SELECT TOP 1 * FROM table WHERE column = @value;";

        var parameters = new { value = "test" };

        var expected = new { Column = "test" };

        _mockConn.Setup(conn => conn.QuerySingle<dynamic>(sql, parameters, null, null, null)).Returns(expected);

        // Act
        var result = _dapperWrapper.QuerySingle<dynamic>(_mockConn.Object, sql, parameters);

        // Assert
        Assert.Equal(expected, result);
    }
}
