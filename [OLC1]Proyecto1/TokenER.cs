using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class TokenER
    {
        public enum TipoER
        {
            ASTERISCO,
            OR,
            MAS,
            INTERROGACION,
            CADENA,
            SIMBOLO,
            IDENTIFICADOR,
            PUNTO,
            NUMERAL



        }
        public TipoER tipo;
        public String lexema;
        public String nExpresion;
        public bool condicion;

        public TokenER(TipoER tipo, String lexema, String nExpresion,bool condicion)
        {
            this.tipo = tipo;
            this.lexema = lexema;
            this.nExpresion = nExpresion;
            this.condicion = condicion;
        }
    }
}
