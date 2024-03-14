using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace BusinessSolutionShared;

public class DbConnector
{
    private readonly IConfiguration _configuration;

    private readonly MySqlConnection _con;

    public DbConnector(IConfiguration configuration, MySqlConnection con)
    {
        _configuration = configuration;

        _con = con;
    }

    public IDbConnection CreateConnection()
    {
        var conString = GetConnectionString();

        SetConnection(conString);

        OpenConnection();

        return _con;
    }

    private string? GetConnectionString() =>

        _configuration.GetConnectionString("client_inquiries");

    private void SetConnection(string? connectionString) =>

        _con.ConnectionString = connectionString ?? throw new InvalidOperationException("Connection string cannot be null.");

    private void OpenConnection()
    {
        try
        {
            _con.Open();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error opening DB connection", ex);
        }
    }
}
