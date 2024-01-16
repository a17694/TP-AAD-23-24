using InterfaceAAD.Connections;
using System.Data.SqlClient;

namespace InterfaceAAD.Repositories
{
    /// <summary>
    /// Abstract base class for repositories.
    /// </summary>
    public class BaseRepository
    {
        /// <summary>
        /// Protected field to store a reference to the database context.
        /// </summary>
        protected readonly SqlConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        protected BaseRepository()
        {
            _db = (new SqlServerConnection()).GetConnection();
        }
    }
}
