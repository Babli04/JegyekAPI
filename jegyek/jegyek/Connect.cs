using MySql.Data.MySqlClient;

namespace jegyek
{
    public class Connect
    {
        public MySqlConnection connection;
        private string Host;
        private string DbName;
        private string Username;
        private string Password;
        private string ConnectionString;

        public Connect()
        {
            Host = "localhost";
            DbName = "db_jegyek";
            Username = "root";
            Password = "";

            ConnectionString = $"Host={Host};Database={DbName};User={Username};Password={Password};SslMode=None";

            connection = new MySqlConnection(ConnectionString);
        }
    }
}
