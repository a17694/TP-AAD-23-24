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
        public bool Save(Property entity)
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
            properties = GetState(properties);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NIP", typeof(string));
            dataTable.Columns.Add("Morada", typeof(string));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("Tipologia", typeof(string));
            dataTable.Columns.Add("Estado", typeof(string));

            foreach (Property property in properties)
            {
                DataRow row = dataTable.NewRow();
                row["NIP"] = property.PropriedadeNIP;
                row["Morada"] = property.PropriedadeMorada;
                row["Tipo"] = property.DescTpPropriedade;
                row["Tipologia"] = property.DescTipologia;
                row["Estado"] = property.Estado; 

                dataTable.Rows.Add(row);
            }

            
            return Task.FromResult(dataTable);
        }

        public List<Property> GetState(List<Property> properties)
        {
            List<Property> updatedProperties = new List<Property>();

            foreach (Property property in properties)
            {
                string query = $"EXEC VerificarEstadoPropriedade @NIPPropriedade = '{property.PropriedadeNIP}'";

                using (SqlCommand command = new SqlCommand(query, _db))
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Property updatedProperty = new Property();
                                updatedProperty = property;

                                PropertyState estado;
                                Enum.TryParse<PropertyState>((string)(reader["Estado"]), out estado);
                                updatedProperty.Estado = estado;

                                updatedProperties.Add(updatedProperty);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
            }


            return updatedProperties;

        }
    }
}
