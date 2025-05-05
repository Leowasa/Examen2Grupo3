using Guna.UI2.WinForms;
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

namespace Examen2Grupo3
{
    public partial class AgregarCliente : Form
    {
        public Cliente Datos = new Cliente();
        public AgregarCliente()
        {
            InitializeComponent();
        }
        public Cliente ObtenerClienteEditado()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                throw new InvalidOperationException("El tipo de cliente no puede ser nulo. Por favor, seleccione un tipo.");
            }

            Cliente clienteEditado = new Cliente
            {
                ID = int.Parse(guna2TextBox1.Text), // Ensure 'ID' is a property of 'cliente' and not a Label
                Nombre = guna2TextBox2.Text,
                Direccion = guna2TextBox3.Text,
                Correo = guna2TextBox4.Text,
                Tipo = guna2ComboBox1.SelectedItem?.ToString() ?? string.Empty // Safeguard against null
            };
            return clienteEditado;
        }
        private void AgregarCliente_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
        public void SetDatosProducto(string ID, string Nombre, string Direccion, string Correo, string Tipo)
        {
            try
            {
                guna2TextBox1.Text = ID;
                guna2TextBox2.Text = Nombre;
                guna2TextBox4.Text = Direccion;
                guna2TextBox3.Text = Correo;
                guna2ComboBox1.Text = Tipo;

                guna2TextBox1.ReadOnly = true;
                guna2TextBox1.BackColor = Color.LightGray;
            }
            catch

            {

            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Datos.ID = int.Parse(guna2TextBox1.Text);
                Datos.Nombre = guna2TextBox2.Text;
                Datos.Direccion = guna2TextBox3.Text;
                Datos.Correo = guna2TextBox4.Text;

                if (guna2ComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un tipo de cliente.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Datos.Tipo = guna2ComboBox1.SelectedItem?.ToString() ?? string.Empty; // Safeguard against null
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
