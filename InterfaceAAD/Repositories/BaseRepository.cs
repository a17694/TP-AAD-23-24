using InterfaceAAD.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAAD.Repositories
{
    /// <summary>
    /// Abstract base class for repositories.
    /// </summary>
    public abstract class BaseRepository
    {
        /// <summary>
        /// Protected field to store a reference to the database context.
        /// </summary>
        protected readonly DataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        protected BaseRepository()
        {
            _db = new DataBaseConnection();
        }
    }
}
