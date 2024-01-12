using System.Data.SqlClient;


namespace InterfaceAAD.DataBase
{
    /// <summary>
    /// Represents a connection to a SQL Server database.
    /// </summary>
    public class SqlServerConnection
    {
        /// <summary>
        /// The connection string for the SQL Server database.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// The SQL Server database connection.
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerConnection"/> class.
        /// </summary>
        public SqlServerConnection()
        {
            // Open the database connection during initialization.
            var host = Environment.GetEnvironmentVariable("SQL_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("SQL_PORT") ?? "";
            var db = Environment.GetEnvironmentVariable("SQL_DB") ?? "Agencia";
            var user = Environment.GetEnvironmentVariable("SQL_USER") ?? "";
            var password = Environment.GetEnvironmentVariable("SQL_PASSWORD") ?? "";
            var security = Environment.GetEnvironmentVariable("SQL_SECURITY") ?? "false";

            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
            {
                host = !string.IsNullOrEmpty(port) ? host + "," + port : host;
                _connectionString = $"Data Source={host};Initial Catalog={db};User ID={user};Password={password};";
            }
            else
            {
                _connectionString = $"Data Source={host};Initial Catalog={db};Integrated Security={security};";
            }

                _connection = new SqlConnection(_connectionString);
                _connection.Open();

        }

        /// <summary>
        /// Gets the SQL Server database connection.
        /// </summary>
        /// <returns>The SqlConnection instance.</returns>
        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
