using InterfaceAAD.Models.Entities;
using InterfaceAAD.Models.Interfaces;
using System.Data.SqlClient;
using System.Windows;

namespace InterfaceAAD.Repositories
{
    public class ContactTypeRepository : BaseRepository, IBaseRepository<TipoContacto>
    {
        public List<TipoContacto> GetAll()
        {
            List<TipoContacto> typeContacts = new List<TipoContacto>();

            string query = "SELECT * FROM TipoContacto";

            using (SqlCommand command = new SqlCommand(query, _db))
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoContacto tipoContacto = new TipoContacto
                            {
                                TpContactoID = (int)reader["TpContactoID"],
                                DescTpContacto = (string)reader["DescTpContacto"]
                            };

                            typeContacts.Add(tipoContacto);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }

            return typeContacts;
        }

        public bool Save(TipoContacto entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TipoContacto entity)
        {
            throw new NotImplementedException();
        }

        public TipoContacto GetById(int TpContactoID)
        {
            using (SqlCommand command =
                   new SqlCommand("SELECT * FROM TipoContacto WHERE TpContactoID = @TpContactoID", _db))
            {
                command.Parameters.AddWithValue("@TpContactoID", TpContactoID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TipoContacto
                        {
                            TpContactoID = (int)reader["TpContactoID"],
                            DescTpContacto = (string)reader["DescTpContacto"]
                        };
                    }
                }
            }

            return new TipoContacto();
        }
    }
}