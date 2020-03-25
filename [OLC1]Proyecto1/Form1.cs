using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto1
{
    public partial class Form1 : Form
    {
        LinkedList<String> listaPestanas = new LinkedList<String>();
        int contador = 0;
        OpenFileDialog seleccionar = new OpenFileDialog();
        List<Imagen> listaAuxT = new List<Imagen>();


        /*{
            InitialDirectory = "C:\\",
            Filter = "er files (*.er)|*.er|All files (*.*)|*.*",
            RestoreDirectory = true,
        };*/

        //FileInfo archivo;
        StreamReader leer;
        StreamWriter escribir;
        FileStream archivo;
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public String GuardarArchivo(String archivo,String documento)
        {
            String mensaje =null;
            try
            {
                escribir = new StreamWriter(archivo);
                escribir.WriteLine(documento);
                escribir.Close();
                mensaje = "Archivo Guardado con Exito";
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return mensaje;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox texto = new RichTextBox();

            seleccionar.InitialDirectory = "C:\\";
            seleccionar.Filter = "er files (*.er)|*.er|All files (*.*)|*.*";
            seleccionar.RestoreDirectory = true;
            
            if(seleccionar.ShowDialog()==DialogResult.OK)
            {
                String aux = "";
                ////ARCHIVO
                //archivo = new FileStream(seleccionar.FileName,FileMode.Open, FileAccess.Read, FileShare.Read);
                ////////////
                leer = new StreamReader(seleccionar.FileName);
                String linea = "";
                String auxLinea = "";
                String nombreArchivo = seleccionar.FileName;
                String auxNombreArchivo = "";
                //auxNombreArchivo = nombreArchivo.Substring(nombreArchivo.Length - 11,11);
                for (int i = 1; i < nombreArchivo.Length; i++)
                {
                    aux = nombreArchivo.Substring(nombreArchivo.Length - i, 1);
                    if (aux=="\\")
                    {
                        auxNombreArchivo = nombreArchivo.Substring(nombreArchivo.Length - (i -1), (i - 1));
                        break;
                    }
                }
                while (linea!=null)
                {
                    linea = leer.ReadLine();
                    auxLinea = auxLinea +"\n" + linea;

                }
                leer.Close();
                texto.Text = auxLinea;
                contador++;
                TabPage tp = new TabPage( auxNombreArchivo+" "+contador.ToString());
                tabControl1.TabPages.Add(tp);

                //TextBox tb = new TextBox();
                texto.Dock = DockStyle.Fill;
                texto.Multiline = true;

                tp.Controls.Add(texto);
                //tabControl1.TabPages.Add("Archivo "+contador.ToString(),texto);
                listaPestanas.AddLast("");
            }
            else
            {
                MessageBox.Show("Ventana Cancelada");
            }
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proyecto 1 OLC1 \n" + "Fredy Estuardo Ramírez Moscoso \n" + "201700350");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int numero = tabControl1.SelectedIndex;
            RichTextBox texto = (RichTextBox)tabControl1.TabPages[numero].Controls[0];
            //Console.WriteLine(texto.Text);

            String auxNumero = listaPestanas.ElementAt(numero);
            Console.WriteLine("jjjj" + auxNumero + "jjjjjj");
            if (auxNumero!="")
            {
                /* if (archivo.)
                 {

                 }*/
                String documento = texto.Text;
                String mensaje = GuardarArchivo(auxNumero, documento);
                if (mensaje != null)
                {
                    MessageBox.Show(mensaje);
                    listaPestanas.ElementAt(numero).Equals(seleccionar.FileName);
                }
                else
                {
                    MessageBox.Show("Archivo no Compatible");

                }
            }
            else
            {
               //String documento=

                if (seleccionar.ShowDialog()== DialogResult.OK)
                {
                    String auxRuta = seleccionar.FileName;
                    //archivo = new FileStream(seleccionar.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    String documento = texto.Text;
                    String mensaje = GuardarArchivo(seleccionar.FileName,documento);
                    if (mensaje != null)
                    {
                        MessageBox.Show(mensaje);
                        //listaPestanas.ElementAt(numero).Equals(seleccionar.FileName);
                        listaPestanas.Find("").Value= seleccionar.FileName;
                        Console.WriteLine("ppp"+listaPestanas.ElementAt(numero));
                    }
                    else
                    {
                        MessageBox.Show("Archivo no Compatible");

                    }  
                } 


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numero = tabControl1.SelectedIndex;
            RichTextBox texto = (RichTextBox)tabControl1.TabPages[numero].Controls[0];
            //Console.WriteLine(texto.Text);

            String auxNumero = listaPestanas.ElementAt(numero);
            if (auxNumero != "")
            {
                AnalizadorLexico analizar = new AnalizadorLexico();
                String documento = texto.Text;
                analizar.Analizar(documento);
                analizar.AnalizarER();
                //analizar.reporteHtml();
                //analizar.ReporteErrores();
                //analizar.reporteTokensXML();
                //analizar.reporteErroresXML();
                listBox1.Items.Clear();
                for (int i=0;i<analizar.listaImagenesThompson2.Count;i++)
                {
                    listBox1.Items.Add(analizar.listaImagenesThompson2.ElementAt(i).nombre);
                }
                listaAuxT = analizar.listaImagenesThompson2;
            }
            else
            {
                MessageBox.Show("Ruta no Encontrada");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MetodoThompson mp = new MetodoThompson(null);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < listaAuxT.Count; i++)
            {
                if (listBox1.SelectedItem.ToString().Equals(listaAuxT.ElementAt(i).nombre))
                {
                    VentanaImagenes imagenT = new VentanaImagenes();
                    imagenT.aux = listaAuxT.ElementAt(i).rutaImagen;
                    imagenT.crearImagen();
                    imagenT.Show();
                }

            }
        }
    }
}

