using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto1
{
    public partial class VentanaTabla : Form
    {
        public String aux = "";
        public VentanaTabla()
        {
            InitializeComponent();
        }

        public void crearImagen()
        {
            pictureBox1.ImageLocation = aux;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Show();
        }

        private void VentanaTabla_Load(object sender, EventArgs e)
        {

        }
    }
}
