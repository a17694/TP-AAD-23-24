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
        private readonly string _connectionString = "Data Source=localhost,1411;Initial Catalog=Agencia;User ID=sa;Password=Password0!;";

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
