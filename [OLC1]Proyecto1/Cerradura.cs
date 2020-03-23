using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Cerradura
    {
        public Estados estadoPrincipal;
        //public LinkedList<Estados> listaEstados = new LinkedList<Estados>();
        public List<int> arregloEnteros = new List<int>();
        public int indiceEstado;

        public Cerradura(int indiceestado,Estados estadoPrincipal, List<int> arregloEnteros)
        {
            indiceEstado = indiceestado;
            this.estadoPrincipal = estadoPrincipal;
            this.arregloEnteros = arregloEnteros;
        }
    }
}
