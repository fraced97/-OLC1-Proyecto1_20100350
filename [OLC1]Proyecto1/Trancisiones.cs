using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Trancisiones
    {
        public String terminal;
        public Estados estadoSiguiete;
        public bool condicion;
        public int numero;
        public String NombreTrans;

        public Trancisiones(string nombre, Estados estadoSiguiete, bool condicion, int numero)
        {
            this.terminal = nombre;
            this.estadoSiguiete = estadoSiguiete;
            this.condicion = condicion;
            this.numero = numero;
        }
        public Trancisiones(string nombre, Estados estadoSiguiete)
        {
            this.terminal = nombre;
            this.estadoSiguiete = estadoSiguiete;
           // this.condicion = condicion;
            //this.numero = numero;
        }
        public Trancisiones(string nombre, Estados estadoSiguiete,String nombreTrans)
        {
            this.terminal = nombre;
            this.estadoSiguiete = estadoSiguiete;
            NombreTrans = nombreTrans;
            // this.condicion = condicion;
            //this.numero = numero;
        }
    }
}
