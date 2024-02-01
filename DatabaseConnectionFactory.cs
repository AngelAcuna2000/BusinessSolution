using MySql.Data.MySqlClient;
using System.Data;

public class DatabaseConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connectionString = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"))
            .Build()
            .GetConnectionString("client_inquiries");

        var connection = new MySqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}