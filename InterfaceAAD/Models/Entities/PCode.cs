using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAAD.Models.Entities
{
    public class PCode
    {
        public string CP {  get; set; }
        public string Localidade { get; set; }

        public PCode() { }

        public PCode(string cP, string localidade)
        {
            CP = cP;
            Localidade = localidade;
        }


    }
}
