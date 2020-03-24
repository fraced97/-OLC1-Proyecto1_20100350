using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Estados
    {
        public String nombre;
        public Token tipoToken;
        public int indiceEstado;
        public bool condicion;
        public LinkedList<Trancisiones> listaTrancisiones = new LinkedList<Trancisiones>();
        //public ArrayList arregloEnteros = new ArrayList();
        public List<int> arregloEnteros = new List<int>();
        //public Trancisiones trancision;
        //LinkedList<Cerradura> listaCerradura = new LinkedList<Cerradura>();

       public  LinkedList<Estados> listaEstadosCerradura = new LinkedList<Estados>();

        public Cerradura listaCerradura;
        public Estados(string nombre, Token tipoToken, int contador, bool condicion)
        {
            this.nombre = nombre;
            this.tipoToken = tipoToken;
            this.indiceEstado = contador;
            this.condicion = condicion;
            //this.trancision = trancision;
        }
        public Estados(string nombre,int indice)
        {
            this.nombre = nombre;
            //this.tipoToken = tipoToken;
            this.indiceEstado = indice;
            //this.condicion = condicion;
            //this.trancision = trancision;
        }
        public Estados()
        {
            
        }
        public void agregart(String ter, Estados estadof,int cantidad)
        {
            if (listaTrancisiones.Count < cantidad) {
                listaTrancisiones.AddLast(new Trancisiones(ter,estadof));
            }
        }
        public void agregarTransicionN(String ter, Estados estadof)
        {
            if (listaTrancisiones.Count < 10)
            {
                listaTrancisiones.AddLast(new Trancisiones(ter, estadof));
            }
        }


        public Estados(String nombreEstado, Cerradura listacerradura)
        {
            nombre = nombreEstado;
            listaCerradura = listacerradura;
        }

    }
}
