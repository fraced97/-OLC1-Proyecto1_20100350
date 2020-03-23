using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Afd
    {
        public LinkedList<TokenER> listaTerminales = new LinkedList<TokenER>();

        public LinkedList<Estados> nuevaListaEstados = new LinkedList<Estados>();

        public LinkedList<Cerradura> listaCerraduras = new LinkedList<Cerradura>();

        public LinkedList<Estados> estadosThompson = new LinkedList<Estados>();

        

        public Afd(LinkedList<TokenER> listaTerminales, LinkedList<Cerradura> listacerraduras, LinkedList<Estados> estadosThompson)
        {
            this.listaTerminales = listaTerminales;
           // this.nuevaListaEstados = nuevaListaEstados;
            listaCerraduras = listacerraduras;
            this.estadosThompson = estadosThompson;
        }

        public void MoverA(Estados estadoActual, String terminal)
        {
            /*int i=0;
          
            for(int j = 0; j < estadoActual.listaCerradura.arregloEnteros.Count;j++)
            {
                while (true)
                {
                    if (estadosThompson.ElementAt(i)!=null)
                    {
                        if (estadosThompson.ElementAt(i).indiceEstado == estadoActual.listaCerradura.arregloEnteros[j])
                        {
                            for (int z=0; z<estadosThompson.ElementAt(i).listaTrancisiones.Count;z++)
                            {
                                if (estadosThompson.ElementAt(i).listaTrancisiones.ElementAt(z).estadoSiguiete.nombre.Equals(terminal))
                                {

                                   
                                    

                                    

                                }
                            }


                        }
                    }
                    else
                    {
                        break;
                    }
                    

                    i++;
                }
            }

            //}
            return null;*/
        }

        public void crearNuevoEstado2()
        {

        }

        public void crearNuevoEstado()
        {

            if (nuevaListaEstados.ElementAt(0)==null)
            {

            }





            /*if (nuevaListaEstados.ElementAt(0)==null)
            {
                
                for (int j=0; j<listaCerraduras.Count;j++)
                {
                    if (listaCerraduras.ElementAt(j).estadoPrincipal.indiceEstado==0)
                    {
                        nuevaListaEstados.AddLast(new Estados("A", listaCerraduras.ElementAt(j)));
                        for (int i=0;i<listaTerminales.Count;i++)
                        {
                            MoverA(nuevaListaEstados.ElementAt(0),listaTerminales.ElementAt(i).lexema);


                        }
                                

                    }
                }

                for (int i=0;i<listaTerminales.Count;i++)
                {
                    for ()
                    {

                    }

                }
                

            }
            else
            {

            }*/

        }

       

    }
}
