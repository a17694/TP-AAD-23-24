#region Usings

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using InterfaceAAD.Converters;
using InterfaceAAD.Models.Entities;
using InterfaceAAD.Models.Interfaces;

#endregion

namespace InterfaceAAD.Repositories
{
    /// <summary>
    /// Repository for managing client entities.
    /// </summary>
    public class ClientRepository : BaseRepository, IBaseRepository<Client>
    {
        #region Public Methods

        /// <summary>
        /// Saves the client entity to the database.
        /// </summary>
        /// <param name="client">The client entity to be saved.</param>
        public bool Save(Client client)
        {
            // Check if the client already exists
            bool clientExists = CheckIfClientExists(client.ClienteNIF);

            // Start the transaction
            SqlTransaction transaction = _db.BeginTransaction();

            try
            {
                if (clientExists)
                {
                    // Update the client if it already exists
                    UpdateClient(client, transaction);
                }
                else
                {
                    // Insert the client if it does not exist
                    InsertClient(client, transaction);
                }

                // Commit the transaction if everything goes without errors
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // In case of error, rollback the transaction
                transaction.Rollback();
                MessageBox.Show($"Error during transaction: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Retrieves a client entity by its unique identifier.
        /// </summary>
        /// <param name="NIF">The unique identifier of the client.</param>
        /// <returns>The client entity if found; otherwise, an empty client.</returns>
        public Client GetById(int NIF)
        {
            Client client = new Client();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Cliente WHERE ClienteNIF = @NIF", _db))
            {
                command.Parameters.AddWithValue("@NIF", NIF);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client
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

            // Client Contacts
            if (client.ClienteNIF != 0)
            {
                using (SqlCommand command =
                       new SqlCommand(
                           "SELECT ContactoCliente, TipoContactoTpContactoID FROM ContactoCliente WHERE ClienteClienteNIF = @NIF",
                           _db))
                {
                    command.Parameters.AddWithValue("@NIF", NIF);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty((string)reader["ContactoCliente"]))
                            {
                                continue;
                            }

                            client.ClientContacts.Add(
                                new ClientContact()
                                {
                                    ClienteClienteNIF = NIF,
                                    TipoContactoTpContactoID = (int)reader["TipoContactoTpContactoID"],
                                    ContactoCliente = (string)reader["ContactoCliente"]
                                }
                            );
                        }
                    }
                }
            }

            // Return an empty client if not found
            return client;
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
                            Client client = new Client(
                                Convert.ToInt32(reader["ClienteNIF"]),
                                (string)reader["ClienteNome"],
                                (DateTime)reader["ClienteDataNasc"],
                                (string)reader["ClienteMorada"],
                                (string)reader["CPCP"]
                            );

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

        #region Private Methods

        /// <summary>
        /// Updates an existing client entity in the database.
        /// </summary>
        /// <param name="client">The client entity to be updated.</param>
        /// <param name="transaction">The SQL transaction.</param>
        private void UpdateClient(Client client, SqlTransaction transaction)
        {
            // Update the client
            using (SqlCommand command = new SqlCommand("UPDATE Cliente SET ClienteNome = @Nome WHERE ClienteNIF = @NIF", _db, transaction))
            {
                command.Parameters.AddWithValue("@NIF", client.ClienteNIF);
                command.Parameters.AddWithValue("@Nome", client.ClienteNome);

                command.ExecuteNonQuery();
            }

            // Remove all existing contacts of the client
            using (SqlCommand command = new SqlCommand("DELETE FROM ContactoCliente WHERE ClienteClienteNIF = @NIF", _db, transaction))
            {
                command.Parameters.AddWithValue("@NIF", client.ClienteNIF);
                command.ExecuteNonQuery();
            }

            // Insert the new contacts of the client
            foreach (var contact in client.ClientContacts)
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO ContactoCliente (ContactoCliente, ClienteClienteNIF, TipoContactoTpContactoID) VALUES (@Contacto, @NIF, @Tipo)", _db, transaction))
                {
                    command.Parameters.AddWithValue("@Contacto", contact.ContactoCliente);
                    command.Parameters.AddWithValue("@NIF", client.ClienteNIF);
                    command.Parameters.AddWithValue("@Tipo", contact.TipoContactoTpContactoID);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Inserts a new client entity into the database.
        /// </summary>
        /// <param name="client">The client entity to be inserted.</param>
        /// <param name="transaction">The SQL transaction.</param>
        private void InsertClient(Client client, SqlTransaction transaction)
        {
            // Insert the client
            using (SqlCommand command = new SqlCommand("INSERT INTO Cliente (ClienteNIF, ClienteNome) VALUES (@NIF, @Nome)", _db, transaction))
            {
                command.Parameters.AddWithValue("@NIF", client.ClienteNIF);
                command.Parameters.AddWithValue("@Nome", client.ClienteNome);

                command.ExecuteNonQuery();
            }

            // Insert the contacts of the client
            foreach (var contact in client.ClientContacts)
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO ContactoCliente (ContactoCliente, ClienteClienteNIF, TipoContactoTpContactoID) VALUES (@Contacto, @NIF, @Tipo)", _db, transaction))
                {
                    command.Parameters.AddWithValue("@Contacto", contact.ContactoCliente);
                    command.Parameters.AddWithValue("@NIF", client.ClienteNIF);
                    command.Parameters.AddWithValue("@Tipo", contact.TipoContactoTpContactoID);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Checks if a client with the specified NIF already exists in the database.
        /// </summary>
        /// <param name="clienteNIF">The NIF of the client to check.</param>
        /// <returns>True if the client exists; otherwise, false.</returns>
        private bool CheckIfClientExists(int clienteNIF)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Cliente WHERE ClienteNIF = @NIF", _db))
            {
                command.Parameters.AddWithValue("@NIF", clienteNIF);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        #endregion
    }
}
