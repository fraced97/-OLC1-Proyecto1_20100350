using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto1
{
    class MetodoThompson
    {
         int indice=0;
         int indiceAfn =1;
         public static int indiceDot=0;
         LinkedList<Estados> listaEstados = new LinkedList<Estados>();
         LinkedList<TokenER> copiaLista;
         LinkedList<TokenER>listaTerminales=new LinkedList<TokenER>();
         

        //LinkedList<Estados> listaEstadosCerradura = new LinkedList<Estados>();

        String texto = "";
        Estados Inicio;
        Estados aux = null;

        LinkedList<Cerradura> cerraduras = new LinkedList<Cerradura>();

        public ArrayList arregloEnteros = new ArrayList();

        public MetodoThompson(LinkedList<TokenER> listaER)
        {
            // copiaLista = new LinkedList<TokenER>();


            copiaLista = listaER;
            /*copiaLista.AddLast(new TokenER(TokenER.TipoER.PUNTO, ".", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.PUNTO, ".", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.PUNTO, ".", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.ASTERISCO, "*", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.OR, "|", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.PUNTO, ".", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.CADENA, "c", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.CADENA, "d", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.PUNTO, ".", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.CADENA, "a", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.CADENA, "b", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.CADENA, "b", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.CADENA, "c", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.MAS, "+", "", true));
            copiaLista.AddLast(new TokenER(TokenER.TipoER.CADENA, "d", "", true));*/


            indice = 0;
            indiceAfn=1;
            metodoThompson(new Estados());
            Graficar();

            //generear_grafica();

        }
        public Estados crearEstado(Estados estado)
        {
            Estados aux = null;
            
            for (int i = 0; i < listaEstados.Count; i++)
            {
                if (listaEstados.ElementAt(i).indiceEstado == estado.indiceEstado) {
                    aux = listaEstados.ElementAt(i);   
                }
            }
            if (aux == null) {
                listaEstados.AddLast(estado);
                aux = estado;
            }
            return aux;
        }


        public void ordenarTransicionesMA(Estados estado0, Estados estado1, Estados estado2, Estados estado3)
        {
            
            crearEstado(estado0).agregart("ε", estado1,1);
            crearEstado(estado1).agregart(estado1.nombre, estado2,1);
            crearEstado(estado2).agregart("ε", estado3,1);
            crearEstado(estado2).agregart("ε", estado1,2);
            crearEstado(estado0).agregart("ε", estado3,2);
           
            
        }
        public void ordenarTransicionesOI(Estados estado0, Estados estado1, Estados estado2, Estados estado3, Estados estado4, Estados estado5)
        {
            crearEstado(estado0).agregart("ε", estado1,1);
            crearEstado(estado0).agregart("ε", estado2,2);
            crearEstado(estado1).agregart(estado1.nombre, estado3,1);
            crearEstado(estado2).agregart(estado2.nombre, estado4,1);
            crearEstado(estado3).agregart("ε", estado5,1);
            crearEstado(estado4).agregart("ε", estado5,1);
          
        }

        public bool terminalExiste(String lexema)
        {
            Boolean Noexiste = true;
            foreach(TokenER i in listaTerminales){
                if (i!=null)
                {
                    if (i.lexema.Equals(lexema))
                    {
                        Noexiste = false;
                        break;
                    }
                    else
                    {
                        Noexiste = true;
                    }
                }
                else
                {
                    Noexiste = true;
                }
                
            }

            return Noexiste;
        }

        public Estados metodoThompson(Estados estado)
        {
            //indice = 0;
            //indiceAfn = 1;
            Estados auxiliar, estado1, estado2, estado3, estado4, estado5;
            
            switch (copiaLista.ElementAt(indice).tipo)
            {
                case TokenER.TipoER.CADENA:
                    estado = new Estados();
                    estado.indiceEstado = indiceAfn;
                    estado.nombre = copiaLista.ElementAt(indice).lexema;
                    if (terminalExiste(copiaLista.ElementAt(indice).lexema))
                    {
                        listaTerminales.AddLast(copiaLista.ElementAt(indice));
                    }
                   // if (indice<copiaLista.Count)
                    //{
                        indice++;
                    //}
                    //else
                   // {
                      //  break;
                    //}
                    
                    indiceAfn++;
                    break;

                case TokenER.TipoER.ASTERISCO:
                    //if (indice < copiaLista.Count)
                    //{
                        indice++;
                    //}
                   // else
                   // {
                        //break;
                    //}
                    estado1 = new Estados("ε", indiceAfn++);
                    estado2 = metodoThompson(estado1);
                    estado3 = new Estados("ε",indiceAfn++);
                    ordenarTransicionesMA(estado,estado1,estado2,estado3);
                    estado = estado3;
                    break;
                case TokenER.TipoER.IDENTIFICADOR:

                    estado = new Estados();
                    estado.indiceEstado = indiceAfn;
                    estado.nombre = copiaLista.ElementAt(indice).lexema;
                    if (terminalExiste(copiaLista.ElementAt(indice).lexema))
                    {
                        listaTerminales.AddLast(copiaLista.ElementAt(indice));
                    }
                    //if (indice < copiaLista.Count)
                    //{
                        indice++;
                    //}
                    //else
                    //{
                   //     break;
                   // }
                    indiceAfn++;

                    break;
                case TokenER.TipoER.PUNTO:
                   // if (indice < copiaLista.Count)
                   // {
                        indice++;
                   // }
                   // else
                   // {
                    //    break;
                    //}
                    estado1 = metodoThompson(estado);
                    crearEstado(estado).agregart(estado1.nombre, estado1,1);
                    estado2 = metodoThompson(estado1);
                    crearEstado(estado1).agregart(estado2.nombre, estado2,1);
                    estado = estado2;


                    break;
                case TokenER.TipoER.INTERROGACION:
                    //if (indice < copiaLista.Count)
                    //{
                        indice++;
                    //}
                    //else
                    //{
                    //    break;
                    //}
                    estado1 = new Estados("ε", indiceAfn++);
                    estado3 = metodoThompson(estado1);
                    estado2 = new Estados("ε", indiceAfn++);
                    estado4 = new Estados("ε", indiceAfn++);
                    estado5 = new Estados("ε", indiceAfn++);
                    ordenarTransicionesOI(estado, estado1, estado2, estado3, estado4, estado5);
                    estado = estado5;
                    break;


                    
                case TokenER.TipoER.OR:
                    //if (indice < copiaLista.Count)
                    //{
                        indice++;
                    //}
                    //else
                    //{
                    //    break;
                    //}
                    estado1 = new Estados("ε", indiceAfn++);
                    estado3 = metodoThompson(estado1);
                    estado2= new Estados("ε", indiceAfn++);
                    estado4 = metodoThompson(estado2);
                    estado5= new Estados("ε", indiceAfn++);
                    ordenarTransicionesOI(estado, estado1, estado2, estado3, estado4, estado5);
                    estado = estado5;
                    break;
                case TokenER.TipoER.MAS:
                   // if (indice < copiaLista.Count)
                   // {
                        indice++;
                    //}
                    //else
                   // {
                      //  break;
                    //}
                    int tmp = indice;
                    estado1 = metodoThompson(estado);
                    indice = tmp;
                    crearEstado(estado).agregart(estado1.nombre, estado1,1);
                    estado2 = new Estados("ε",indiceAfn++);
                    estado3 = metodoThompson(estado2);
                    estado4 = new Estados("ε", indiceAfn++);
                    ordenarTransicionesMA(estado1,estado2,estado3,estado4);
                    estado = estado4;
                    break;

            }



            return estado;
        }
        public void analizarEstados()
        {
            
            for (int i =0;i<listaEstados.Count;i++)
            {
                aux = listaEstados.ElementAt(i);
                aux.arregloEnteros.Add(aux.indiceEstado);
                aux.arregloEnteros=GenerarCerradura(aux,aux.arregloEnteros);
                aux.arregloEnteros.Sort();
                cerraduras.AddLast(new Cerradura(aux.indiceEstado,aux,aux.arregloEnteros));           
    
            }
            for (int i=0;i<cerraduras.Count;i++)
            {
                for (int j=0;j<cerraduras.ElementAt(i).arregloEnteros.Count;j++)
                {

                    //Console.WriteLine(cerraduras.ElementAt(i).estadoPrincipal.indiceEstado+"-----------"+ cerraduras.ElementAt(i).arregloEnteros[j].ToString());
                    Console.WriteLine(cerraduras.ElementAt(i).indiceEstado+"-------"+cerraduras.ElementAt(i).estadoPrincipal.indiceEstado + "-----------" + cerraduras.ElementAt(i).arregloEnteros[j].ToString());

                }
            }
            

        }
        public void Letras()
        {
            char letra = 'A';

            while (letra <= 'Z')
            {
                Console.WriteLine("-" + letra);

                letra++;

            }
        }

        public List<int> GenerarCerradura(Estados aux, List<int> lista)
        {

            /*for (int i=0;i< aux.listaTrancisiones.Count;i++)
            {
                if (aux.listaTrancisiones.ElementAt(i).estadoSiguiete.nombre.Equals("ε"))
                {
                    aux.arregloEnteros.Add(aux.listaTrancisiones.ElementAt(i).estadoSiguiete.indiceEstado);
                    GenerarCerradura();
                }
            }*/
            foreach (Trancisiones i in aux.listaTrancisiones)
            {
                if (i.estadoSiguiete.nombre.Equals("ε"))
                {
                    lista.Add(i.estadoSiguiete.indiceEstado);

                    GenerarCerradura(i.estadoSiguiete,lista);
                }
            }
            return lista;



            /* for (int i = 0; i < cerraduras.Count; i++)
             {
                 Console.WriteLine("*******"+"X"+listaEstadosCerradura.ElementAt(i).indiceEstado+"**************");
             }*/
        }

        public void generear_grafica() {
            texto = "";
            for (int i = 0; i < listaEstados.Count; i++)
            {
                for (int j = 0; j < listaEstados.ElementAt(i).listaTrancisiones.Count; j++)
                {
                    texto = texto + "X" + listaEstados.ElementAt(i).indiceEstado + "-> X" 
                        + listaEstados.ElementAt(i).listaTrancisiones.ElementAt(j).estadoSiguiete.indiceEstado +
                        "[label=\"" + listaEstados.ElementAt(i).listaTrancisiones.ElementAt(j).estadoSiguiete.nombre+ "\"];\n";

                }
            }
            Console.WriteLine(texto);
        }
        public void Graficar()
        {
            indiceDot++;
            TextWriter escribir;
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Thompson" + "\\" + "Thompson"+indiceDot.ToString()+".dot";
            String ruta2 = Application.StartupPath + "\\Thompson" + "\\" + "Thompson"+ indiceDot.ToString() + ".dot";
            String ruta4 = Application.StartupPath + @"\Thompson";
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
            escribir.WriteLine("node[shape = \"doublecircle\"]"+"X"+(indiceAfn-1)+";");
            escribir.WriteLine("node[shape=\"circle\"];");
            generear_grafica();
            escribir.WriteLine(texto);
            ///////////////////
            //GraficarOr(escribir);
            //escribir.WriteLine("\""+"Welcome"+"\""+"->" +"\""+"To"+ "\"" +"\n" +"\""+"To"+"\""+"->" +"\""+"Web"+"\"" +"\n" +"\""+"To"+"\""+"->" +"\""+"GraphViz!"+"\"");
            escribir.WriteLine("}");
            escribir.Close();

            String rutaImagen = Application.StartupPath + "//Thompson" + "//" + "ThompsonImagen"+ indiceDot.ToString() + ".jpg";
            String rutaDot = Application.StartupPath + "//Thompson" + "//" + "Thompson"+ indiceDot.ToString() + ".dot";
            
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
            //Console.ReadKey();

            foreach (TokenER i in listaTerminales)
            {
                Console.WriteLine("---------"+i.lexema+"-------");
            }

            // estadoCerradura();
            analizarEstados();
            //Letras();

        }

        

    }
}
