using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace BusinessSolutionShared;

public class DbConnector
{
    private readonly ConfigurationBuilder _configBuilder;

    private readonly MySqlConnection _con;

    public DbConnector(ConfigurationBuilder configBuilder, MySqlConnection con)
    {
        _configBuilder = configBuilder;

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

        _configBuilder.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"))
                      .Build()
                      .GetConnectionString("client_inquiries");

    private void SetConnection(string? connectionString) => _con.ConnectionString = connectionString;

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
