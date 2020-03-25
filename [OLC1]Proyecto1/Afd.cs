using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto1
{
    class Afd
    {
        String texto = "";
        static int indiceDot=0;
        int IndiceEstadoAFN = 0;
        public LinkedList<TokenER> listaTerminales = new LinkedList<TokenER>();

        public LinkedList<Estados> nuevaListaEstadosAFN = new LinkedList<Estados>();

        public LinkedList<Cerradura> listaCerraduras = new LinkedList<Cerradura>();

        public LinkedList<Estados> estadosThompson = new LinkedList<Estados>();

        public LinkedList<Cerradura> listaEstadosCAuxiliar = new LinkedList<Cerradura>();
        List<int> listaIndiceEstadoAux = new List<int>();


        public LinkedList<Estados> listaAxEstados = new LinkedList<Estados>();

        public LinkedList<Estados> listaAUX2Estados=new LinkedList<Estados>();

        String auxTerminal = "";

        int indiceEstadoNuevo = 0;

        public static List<Imagen> listaImagenesAFD = new List<Imagen>();


        public Afd(LinkedList<TokenER> listaTerminales, LinkedList<Cerradura> listacerraduras, LinkedList<Estados> estadosThompson, int IndiceEstadoAfn)
        {
            this.listaTerminales = listaTerminales;
           // this.nuevaListaEstados = nuevaListaEstados;
            listaCerraduras = listacerraduras;
            this.estadosThompson = estadosThompson;
            this.IndiceEstadoAFN = IndiceEstadoAfn;
            crearEstadoInicial();
            crearNuevoEstado2();
            graficarAFD();

        }
        public void crearEstadoInicial()
        {
            indiceEstadoNuevo++;
            //if (nuevaListaEstadosAFN.ElementAt(0)==null)
           // {
                for (int i=0;i<listaCerraduras.Count;i++)
                {
                    if (listaCerraduras.ElementAt(i).indiceEstadoCerradura==0)
                    {
                        //nuevaListaEstadosAFN.AddLast(new Estados(indiceEstadoNuevo.ToString(),listaCerraduras.ElementAt(i).arregloEnteros,listaCerraduras.ElementAt(i).listaEstadosCerradura));
                         nuevaListaEstadosAFN.AddLast(new Estados(indiceEstadoNuevo.ToString(), listaCerraduras.ElementAt(i).arregloEnteros));
                        //MoverA2(nuevaListaEstadosAFN.ElementAt(0),);
                        //MoverA2(nuevaListaEstadosAFN.ElementAt(0),);
                        for (int j=0;j<listaTerminales.Count;j++)
                        {
                            auxTerminal = "";
                            MoverA2(nuevaListaEstadosAFN.ElementAt(0),listaTerminales.ElementAt(j).lexema);
                        }

                    }


                }


            for (int i=0;i<listaAxEstados.Count;i++)
            {
                nuevaListaEstadosAFN.ElementAt(0).listaTrancisiones.AddLast(new Trancisiones(listaAxEstados.ElementAt(i).nombre, listaAxEstados.ElementAt(i),listaAxEstados.ElementAt(i).nombreTrans));
                nuevaListaEstadosAFN.AddLast(new Estados(listaAxEstados.ElementAt(i).nombre, listaAxEstados.ElementAt(i).arregloEnteros));
            }
            listaAxEstados.Clear();
            auxTerminal = "";
            /*for (int i=0;i<nuevaListaEstadosAFN.Count;i++)
            {
                for (int j=0;j<nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.Count;j++)
                {
                    //estadosThompson.ElementAt(0).listaTrancisiones.AddLast(new Trancisiones());
                    Console.WriteLine("-----****" + nuevaListaEstadosAFN.ElementAt(i).nombre + "-------**********"+nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).terminal);
                    Console.WriteLine("-----****" + nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).NombreTrans + "-------**********");
                }
                
            }*/

           // }
            //Console.WriteLine("Supongo que esta bien REvisalo");

            




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

            for (int i = 1; i < nuevaListaEstadosAFN.Count; i++)
            {
                for (int j = 0; j < listaTerminales.Count; j++)
                {
                    auxTerminal = "";
                    MoverA2(nuevaListaEstadosAFN.ElementAt(i), listaTerminales.ElementAt(j).lexema);
                    
                }
                if (listaAxEstados.Count!=0)
                {
                    for (int j = 0; j < listaAxEstados.Count; j++)
                    {
                        nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.AddLast(new Trancisiones(listaAxEstados.ElementAt(j).nombre, listaAxEstados.ElementAt(j), listaAxEstados.ElementAt(j).nombreTrans));
                        nuevaListaEstadosAFN.AddLast(new Estados(listaAxEstados.ElementAt(j).nombre, listaAxEstados.ElementAt(j).arregloEnteros));
                    }
                }
                if (listaAUX2Estados.Count!=0)
                {
                    for (int j = 0; j < listaAUX2Estados.Count; j++)
                    {
                        nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.AddLast(new Trancisiones(listaAUX2Estados.ElementAt(j).nombre, listaAUX2Estados.ElementAt(j), listaAUX2Estados.ElementAt(j).nombreTrans));
                    }
                    
                }
                listaAUX2Estados.Clear();
                listaAxEstados.Clear();
                auxTerminal = "";
            }

            for (int i = 0; i < nuevaListaEstadosAFN.Count; i++)
            {
                for (int j = 0; j < nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.Count; j++)
                {
                    //estadosThompson.ElementAt(0).listaTrancisiones.AddLast(new Trancisiones());
                    Console.WriteLine("-----****" + nuevaListaEstadosAFN.ElementAt(i).nombre + "-------**********" + nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).terminal);
                    Console.WriteLine("-----****" + nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).NombreTrans + "-------**********");
                }

            }

        }

       public void MoverA2(Estados estado, String terminal)
        {
            auxTerminal = terminal;
            /* for (int i=0;i<estado.listaEstadosCerradura.Count;i++)
             {
                 for (int j=0;j<estado.listaEstadosCerradura.ElementAt(i).listaTrancisiones.Count;j++)
                 {
                     if (estado.listaEstadosCerradura.ElementAt(i).listaTrancisiones.ElementAt(j).estadoSiguiete.nombre.Equals(terminal))
                     {
                         for (int k=0;k<listaCerraduras.Count;k++)
                         {
                             if (listaCerraduras.ElementAt(k).indiceEstadoCerradura== estado.listaEstadosCerradura.ElementAt(i).listaTrancisiones.ElementAt(j).estadoSiguiete.indiceEstado)
                             {

                                listaEstadosCAuxiliar.AddLast(new Cerradura(listaCerraduras.ElementAt(k).indiceEstadoCerradura, listaCerraduras.ElementAt(k).estadoPrincipal, listaCerraduras.ElementAt(k).arregloEnteros, listaCerraduras.ElementAt(k).listaEstadosCerradura));

                             }


                         }


                     }


                 }


             }*/


            for (int i = 0; i < estado.arregloEnteros.Count; i++)
            {

                for (int j = 0; j < estadosThompson.Count; j++)
                {
                    
                    if (estadosThompson.ElementAt(j).indiceEstado==estado.arregloEnteros[i])
                    {
                        for(int k = 0; k < estadosThompson.ElementAt(j).listaTrancisiones.Count; k++)
                        {
                            if (estadosThompson.ElementAt(j).listaTrancisiones.ElementAt(k).estadoSiguiete.nombre.Equals(terminal))
                            {
                                for (int p=0;p<listaCerraduras.Count;p++)
                                {
                                    if (listaCerraduras.ElementAt(p).indiceEstadoCerradura== estadosThompson.ElementAt(j).listaTrancisiones.ElementAt(k).estadoSiguiete.indiceEstado)
                                    {
                                        listaEstadosCAuxiliar.AddLast(new Cerradura(listaCerraduras.ElementAt(p).indiceEstadoCerradura, listaCerraduras.ElementAt(p).estadoPrincipal, listaCerraduras.ElementAt(p).arregloEnteros));


                                    }

                                }

                            }
                         }


                    }


                }
            }
            crearNuevoEstado(listaEstadosCAuxiliar);
            listaEstadosCAuxiliar.Clear();


        }

       public void crearNuevoEstado(LinkedList<Cerradura> listCAuxiliar)
        {
            List<int> arregloEnterosAux = new List<int>();
            //LinkedList<Estados> lestadosAux = new LinkedList<Estados>();
            if (listCAuxiliar.Count!=0)
            {
                //if (listCAuxiliar.ElementAt(0)!=null)
               // {
                    for (int i=0;i<listCAuxiliar.Count;i++)
                    {
                       //if (i==0)
                       // {
                            for (int j = 0; j < listCAuxiliar.ElementAt(i).arregloEnteros.Count; j++)
                            {
                            /*if (lestadosAux.ElementAt(0) == null)
                            {
                                lestadosAux.AddLast(listCAuxiliar.ElementAt(i).listaEstadosCerradura.ElementAt(j));
                            }
                            else
                            {
                                for (int k = 0; k < lestadosAux.Count; k++)
                                {
                                    if (lestadosAux.ElementAt(k) != listCAuxiliar.ElementAt(i).listaEstadosCerradura.ElementAt(j))
                                    {
                                        lestadosAux.AddLast(listCAuxiliar.ElementAt(i).listaEstadosCerradura.ElementAt(j));

                                    }
                                }
                            }*/

                            arregloEnterosAux.Add(listCAuxiliar.ElementAt(i).arregloEnteros[j]);


                            }
                       // }
                        
                        
                    }


               // }


            }
            if (listCAuxiliar.Count!=0)
            {
                Boolean existe = false;
                arregloEnterosAux = arregloEnterosAux.Distinct().ToList();
                arregloEnterosAux.Sort();
                for (int i = 0; i < nuevaListaEstadosAFN.Count; i++)
                {
                    //
                   // if(nuevaListaEstadosAFN.ElementAt(i).arregloEnteros.Equals(arregloEnterosAux))
                    if (CompareLists(nuevaListaEstadosAFN.ElementAt(i).arregloEnteros, arregloEnterosAux))
                    {
                        //indiceEstadoNuevo++;
                        //nuevaListaEstadosAFN.AddLast(new Estados(indiceEstadoNuevo.ToString(), arregloEnterosAux));
                        //nuevaListaEstadosAFN.ElementAt(0).listaTrancisiones.AddLast(new Trancisiones(auxTerminal, nuevaListaEstadosAFN.ElementAt(i)));
                        listaAUX2Estados.AddLast(new Estados(nuevaListaEstadosAFN.ElementAt(i).nombre, arregloEnterosAux, auxTerminal));
                        existe = true;
                        break;

                    }
                    else
                    {
                        existe = false;
                    }

                }
                if (existe == false)
                {
                    indiceEstadoNuevo++;
                    listaAxEstados.AddLast(new Estados(indiceEstadoNuevo.ToString(), arregloEnterosAux,auxTerminal));
                   // nuevaListaEstadosAFN.AddLast(new Estados(indiceEstadoNuevo.ToString(), arregloEnterosAux));

                }

            }
            

            

        }
        public void graficarAFD2()
        {
            texto = "";
            for (int i = 0; i < nuevaListaEstadosAFN.Count; i++)
            {
                for (int j = 0; j < nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.Count; j++)
                {
                    texto = texto + "X" + nuevaListaEstadosAFN.ElementAt(i).nombre + "-> X"
                        + nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).terminal +
                        "[label=\"" + nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).NombreTrans + "\"];\n";
                    //estadosThompson.ElementAt(0).listaTrancisiones.AddLast(new Trancisiones());
                    //Console.WriteLine("-----****" + nuevaListaEstadosAFN.ElementAt(i).nombre + "-------**********" + nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).terminal);
                    //Console.WriteLine("-----****" + nuevaListaEstadosAFN.ElementAt(i).listaTrancisiones.ElementAt(j).NombreTrans + "-------**********");
                }

            }

        }

        public void graficarAFD()
        {
            indiceDot++;
            TextWriter escribir;
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AFD" + "\\" + "AFD" + indiceDot.ToString() + ".dot";
            String ruta2 = Application.StartupPath + "\\AFD" + "\\" + "AFD" + indiceDot.ToString() + ".dot";
            String ruta4 = Application.StartupPath + @"\AFD";

            try
            {
                if (Directory.Exists(ruta4))
                {
                    MessageBox.Show("Carpeta Existe");
                }
                else
                {
                    MessageBox.Show("Carpeta No Existe, Creando....");
                    Directory.CreateDirectory(ruta4);
                }
            }
            catch (Exception e)
            {

            }
            escribir = new StreamWriter(ruta2);
            escribir.WriteLine("digraph G {\n");
            escribir.WriteLine("rankdir=LR;");
            for (int i=0;i< nuevaListaEstadosAFN.Count;i++)
            {
                for (int j=0;j<nuevaListaEstadosAFN.ElementAt(i).arregloEnteros.Count;j++)
                {
                    if (nuevaListaEstadosAFN.ElementAt(i).arregloEnteros[j]==IndiceEstadoAFN)
                    {
                        escribir.WriteLine("node[shape = \"doublecircle\"]" + "X" + nuevaListaEstadosAFN.ElementAt(i).nombre + ";");
                    }

                }
            }
            //escribir.WriteLine("node[shape = \"doublecircle\"]" + "X" + (indiceAfn - 1) + ";");
            escribir.WriteLine("node[shape=\"circle\"];");
            //generear_grafica();
            graficarAFD2();
            escribir.WriteLine(texto);
            ///////////////////
            //GraficarOr(escribir);
            //escribir.WriteLine("\""+"Welcome"+"\""+"->" +"\""+"To"+ "\"" +"\n" +"\""+"To"+"\""+"->" +"\""+"Web"+"\"" +"\n" +"\""+"To"+"\""+"->" +"\""+"GraphViz!"+"\"");
            escribir.WriteLine("}");
            escribir.Close();

            String rutaImagen = Application.StartupPath + "//AFD" + "//" + "AFDImagen" + indiceDot.ToString() + ".jpg";
            String rutaDot = Application.StartupPath + "//AFD" + "//" + "AFD" + indiceDot.ToString() + ".dot";
            listaImagenesAFD.Add(new Imagen("AFDImagen" + indiceDot.ToString() + ".jpg", rutaImagen));
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine("dot -Tjpg " + rutaDot + " -o " + rutaImagen);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            Console.WriteLine(process.StandardOutput.ReadToEnd());

        }


        public List<Imagen> crearImagen()
        {
            List<Imagen> listaImagenesaux = new List<Imagen>();
            listaImagenesaux = listaImagenesAFD;


            return listaImagenesaux;
        }

        public static bool CompareLists<T>(List<T> aListA, List<T> aListB)
        {
            if (aListA == null || aListB == null || aListA.Count != aListB.Count)
                return false;
            if (aListA.Count == 0)
                return true;
            Dictionary<T, int> lookUp = new Dictionary<T, int>();
            // create index for the first list
            for (int i = 0; i < aListA.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListA[i], out count))
                {
                    lookUp.Add(aListA[i], 1);
                    continue;
                }
                lookUp[aListA[i]] = count + 1;
            }
            for (int i = 0; i < aListB.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListB[i], out count))
                {
                    // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                    return false;
                }
                count--;
                if (count <= 0)
                    lookUp.Remove(aListB[i]);
                else
                    lookUp[aListB[i]] = count;
            }
            // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
            return lookUp.Count == 0;
        }



    }
}
