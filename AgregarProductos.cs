using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.RegistroPedidos;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Examen2Grupo3
{

    public partial class Agregar_Productos : Form
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public Agregar_Productos()
        {
            InitializeComponent();

            // Initialize non-nullable properties to default values to fix CS8618  
        
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Id = int.Parse(guna2TextBox1.Text);
                Nombre = guna2TextBox2.Text;
                Descripcion = guna2TextBox3.Text;
                Categoria = guna2TextBox4.Text;
                PrecioUnitario = decimal.Parse(guna2TextBox6.Text);
                Cantidad = int.Parse(guna2TextBox5.Text);
            } 
            catch 
            {
                MessageBox.Show("Datos incompletos o erróneos. Intente nuevamente");
            }
               
            
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Agregar_Productos_Load(object sender, EventArgs e)
        {

        }
    }


}


