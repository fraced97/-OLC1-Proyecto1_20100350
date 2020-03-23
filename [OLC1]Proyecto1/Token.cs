using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Token
    {
        public enum Tipo
        {
            DIAGONAL,
            MENOR_QUE,
            GUION,
            EXCLAMACION,
            MAYOR_QUE,
            DOS_PUNTOS,
            COMILLAS,
            PUNTO_COMA,
            IDENTIFICADOR,
            COMENTARIO,
            CONJUNTO,
            EXPRESION_REGULAR,
            RESERVADA_CONJ,
            EJEMPLO_EXPRESION,
            IDENTIFICADOR_CONJ,
            NUMERAL,
            




        }
        public Tipo token;
        public String lexema;
        public String tipo1;
        public int fila;
        public int columna;

        public Token(Tipo token, string lexema, string tipo1, int fila, int columna)
        {
            this.token = token;
            this.lexema = lexema;
            this.tipo1 = tipo1;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
