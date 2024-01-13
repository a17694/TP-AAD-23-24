using InterfaceAAD.Models.Entities;
using InterfaceAAD.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InterfaceAAD.Repositories
{
    public class ContactTypeRepository : BaseRepository, IBaseRepository<TipoContacto>
    {
        public bool Add(TipoContacto entity)
        {
            throw new NotImplementedException();
        }

        public bool Edit(TipoContacto entity)
        {
            throw new NotImplementedException();
        }

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