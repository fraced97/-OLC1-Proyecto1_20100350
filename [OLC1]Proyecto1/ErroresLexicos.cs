using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class ErroresLexicos
    {
        public ErroresLexicos(String error, String tipo, String descripcion, int fila, int columna)
        {
            this.error = error;
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.fila = fila;
            this.columna = columna;
        }
        public String error;
        public String tipo;
        public String descripcion;
       public int fila;
       public int columna;
    }
}
