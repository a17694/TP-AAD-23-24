using System.Data;
using System.Data.SqlClient;
using InterfaceAAD.Converters;
using InterfaceAAD.Models.Entities;
using InterfaceAAD.Models.Interfaces;

namespace InterfaceAAD.Repositories;

/// <summary>
/// Repository for managing client entities.
/// </summary>
public class ClientRepository : BaseRepository, IBaseRepository<Client>
{
    /// <inheritdoc />
    public bool Add(Client entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool Edit(Client entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Client GetById(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public List<Client> GetAll()
    {
        List<Client> clients = new List<Client>();

        string query = "SELECT * FROM Cliente";

        using (SqlCommand command = new SqlCommand(query, _db))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Client client = new Client(Convert.ToInt32(reader["ClienteNIF"]))
                    {
                        // Optionally populate other properties here
                        ClienteNome = (string)reader["ClienteNome"]
                    };

                    clients.Add(client);
                }
            }
        }

        return clients;
    }

    /// <inheritdoc />
    public Task<DataTable> GetAllClientsAsDataTable()
    {
        List<Client> clients = GetAll();
        DataTable dataTable = DataTableConverter.ToDataTable(clients);
        return Task.FromResult(dataTable);
    }
}