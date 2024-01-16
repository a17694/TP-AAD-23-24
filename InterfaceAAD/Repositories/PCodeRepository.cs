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
    public class PCodeRepository : BaseRepository, IBaseRepository<PCode>
    {
        public List<PCode> GetAll()
        {
            List<PCode> pCodeList = new List<PCode>();

            string query = "SELECT * FROM CP";

            using (SqlCommand command = new SqlCommand(query, _db))
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            PCode pCode = new PCode
                            {
                                CP = (string)reader["CP"],
                                Localidade = (string)reader["Localidade"]
                            };

                            pCodeList.Add(pCode);
                        }


                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }
            return pCodeList;

        }

        public PCode GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(PCode entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(PCode entity) 
        { 
            throw new NotImplementedException(); 
        }
    }
}
