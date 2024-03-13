using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace BusinessSolutionShared
{
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
            try
            {
                var conString = _configBuilder
                    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"))
                    .Build()
                    .GetConnectionString("client_inquiries");

                _con.ConnectionString = conString;

                _con.Open();

                return _con;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating DB connection", ex);
            }
        }
    }
}
