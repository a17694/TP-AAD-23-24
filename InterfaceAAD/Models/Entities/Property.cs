using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAAD.Models.Entities
{
    public enum PropertyState
    {
        Vendido,
        Reservado,
        Disponivel
    }
    public class Property
    {
        public string PropriedadeNIP { get; set; }
        public string PropriedadeMorada { get; set; }

        public int Area { get; set; }
        public string DescTpPropriedade { get; set;}

        public string DescTipologia {  get; set;}
        
        public int ClienteClientNIF {  get; set;}

        public string CPCP { get; set;}

        public string Localidade {  get; set;}

        public PropertyState Estado { get; set; }

        public Property() { }

        public Property(string propriedadeNIP, string propriedadeMorada, int area, string descTpPropriedade, int clienteClientNIF, string cPCP, string localidade, string? descTipologia = null, PropertyState ? estado = null)
        {
            PropriedadeNIP = propriedadeNIP;
            PropriedadeMorada = propriedadeMorada;
            Area = area;
            DescTpPropriedade = descTpPropriedade;
            DescTipologia= descTipologia;
            ClienteClientNIF = clienteClientNIF;
            CPCP = cPCP;
            Localidade = localidade;
        }



    }
}
