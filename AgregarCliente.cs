using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.GestorClientes;

namespace Examen2Grupo3
{
    public partial class AgregarCliente : Form
    {
        public cliente Datos = new cliente();
        public AgregarCliente()
        {
            InitializeComponent();
        }

        private void AgregarCliente_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Assuming 'guna2TextBox1' contains the ID as text and 'Id' is the correct property to assign it to.  
                Datos.ID = int.Parse(guna2TextBox1.Text);
                Datos.Nombre = guna2TextBox2.Text;
                Datos.Direccion = guna2TextBox3.Text;
                Datos.Correo = guna2TextBox4.Text;
                // Assuming 'Cliente' is a valid object and 'Tipo' is a property of it.  
                Datos.Tipo = guna2ComboBox1.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., show a message box.  
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
