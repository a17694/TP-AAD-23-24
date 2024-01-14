using InterfaceAAD.Converters;
using InterfaceAAD.Models.Entities;
using InterfaceAAD.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InterfaceAAD.Repositories
{
    public class PropertyRepository : BaseRepository, IBaseRepository<Property>
    {
        public bool Add(Property entity)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Property entity)
        {
            throw new NotImplementedException();
        }

        public List<Property> GetAll()
        {
            List<Property> properties = new List<Property>();

            string query = "SELECT PropriedadeNIP,PropriedadeMorada,Area,DescTpPropriedade,DescTipologia,ClienteClienteNIF,CPCP,Localidade FROM Propriedade join TipoPropriedade ON Propriedade.TipoPropriedadeTpPropriedadeID = TipoPropriedade.TpPropriedadeID Join CP ON Propriedade.CPCP = CP.CP left join (SELECT PropriedadePropriedadeNIP, DescTipologia FROM TipoTipologia join TipologiaPropriedade ON TipoTipologia.TpTipologiaID = TipologiaPropriedade.TipoTipologiaTpTipologiaID) AS DescTipoProp ON Propriedade.PropriedadeNIP = DescTipoProp.PropriedadePropriedadeNIP";

            using (SqlCommand command = new SqlCommand(query, _db))
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipologia;
                            if (reader.IsDBNull(reader.GetOrdinal("DescTipologia")))
                            {
                                tipologia = string.Empty;
                            }
                            else tipologia = (string)reader["DescTipologia"];

                            Property property = new Property(
                               (string)(reader["PropriedadeNIP"]),
                               (string)reader["PropriedadeMorada"],
                               (int)reader["Area"],
                               (string)reader["DescTpPropriedade"],
                               (int)reader["ClienteClienteNIF"],
                               (string)reader["CPCP"],
                               (string)reader["Localidade"],
                               tipologia
                               
                           ) ;

                            properties.Add(property);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }

            return properties;
        }

        public Property GetById(int id)
        {
            throw new NotImplementedException();
        }


        public Task<DataTable> GetAllPropertiesAsDataTable()
        {
            List<Property> properties = GetAll();
            DataTable dataTable = DataTableConverter.ToDataTable(properties);
            return Task.FromResult(dataTable);
        }

    }
}
