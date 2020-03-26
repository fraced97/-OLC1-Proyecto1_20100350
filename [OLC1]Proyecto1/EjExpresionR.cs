using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class EjExpresionR
    {
       public String Id;
       public String ejemploExpresion;
       public LinkedList<Estados> listaEstadosAfd = new LinkedList<Estados>();
        public EjExpresionR(string id, string ejemploExpresion)
        {
            Id = id;
            this.ejemploExpresion = ejemploExpresion;
        }
    }
}
