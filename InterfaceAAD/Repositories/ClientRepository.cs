using System.Data;
using System.Data.SqlClient;
using System.Windows;
using InterfaceAAD.Converters;
using InterfaceAAD.Models.Entities;
using InterfaceAAD.Models.Interfaces;

namespace InterfaceAAD.Repositories;

/// <summary>
/// Repository for managing client entities.
/// </summary>
public class ClientRepository : BaseRepository, IBaseRepository<Client>
{
    #region Public Methods

    /// <summary>
    /// Adds a new client entity to the database.
    /// </summary>
    /// <param name="entity">The client entity to add.</param>
    /// <returns>True if the addition is successful; otherwise, false.</returns>
    public bool Add(Client entity)
    {
        // Note: The logic below always returns false. Ensure you have the correct implementation for your needs.
        return false;
        /*
        entity.ClientContacts.ForEach(contact =>
        {
            string query =
                "INSERT INTO ClienteContacto (ClienteNIF, Tipo, Contacto) VALUES (@ClienteNIF, @Tipo, @Contacto)";

            using (SqlCommand command = new SqlCommand(query, _db))
            {
                command.Parameters.AddWithValue("@ClienteNIF", entity.ClienteNIF);
                command.Parameters.AddWithValue("@Tipo", contact.type);
                command.Parameters.AddWithValue("@Contacto", contact.contact);

                command.ExecuteNonQuery();
            }
        });

        return false;
        */
    }

    /// <summary>
    /// Edits an existing client entity in the database.
    /// </summary>
    /// <param name="entity">The client entity to edit.</param>
    /// <returns>True if the editing is successful; otherwise, false.</returns>
    public bool Edit(Client entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves a client entity by its unique identifier.
    /// </summary>
    /// <param name="NIF">The unique identifier of the client.</param>
    /// <returns>The client entity if found; otherwise, an empty client.</returns>
    public Client GetById(int NIF)
    {
        using (SqlCommand command = new SqlCommand("SELECT * FROM Cliente WHERE ClienteNIF = @NIF", _db))
        {
            command.Parameters.AddWithValue("@NIF", NIF);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    DateTime dataDb = (DateTime)reader["ClienteDataNasc"];


                    return new Client
                    {
                        ClienteNIF = (int)reader["ClienteNIF"],
                        ClienteNome = (string)reader["ClienteNome"],
                        ClienteDataNasc = (DateTime)reader["ClienteDataNasc"],
                        ClienteMorada = (string)reader["ClienteMorada"],
                        CPCP = (string)reader["CPCP"]
                    };
                }
            }
        }

        // Return an empty client if not found
        return new Client();
    }

    /// <summary>
    /// Retrieves a list of all client entities from the database.
    /// </summary>
    /// <returns>A list of client entities.</returns>
    public List<Client> GetAll()
    {
        List<Client> clients = new List<Client>();

        string query = "SELECT * FROM Cliente";

        using (SqlCommand command = new SqlCommand(query, _db))
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Client client = new Client(Convert.ToInt32(reader["ClienteNIF"]))
                        {
                            // Optionally populate other properties here
                            ClienteNIF = (int)reader["ClienteNIF"],
                            ClienteNome = (string)reader["ClienteNome"],
                            ClienteDataNasc = (DateTime)reader["ClienteDataNasc"],
                            ClienteMorada = (string)reader["ClienteMorada"],
                            CPCP = (string)reader["CPCP"]
                        };

                        clients.Add(client);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }

        return clients;
    }

    /// <summary>
    /// Retrieves all client entities as a DataTable.
    /// </summary>
    /// <returns>A DataTable containing all client entities.</returns>
    public Task<DataTable> GetAllClientsAsDataTable()
    {
        List<Client> clients = GetAll();
        DataTable dataTable = DataTableConverter.ToDataTable(clients);
        return Task.FromResult(dataTable);
    }

    #endregion
}