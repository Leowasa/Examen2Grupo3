using Examen2Grupo3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.RegistroPedidos;


namespace ejemplo
{
    public partial class Clientes : Form
    {
        public List<Cliente> cliente = new List<Cliente>();
        public Clientes()
        {
            InitializeComponent();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AgregarCliente agregarCliente = new AgregarCliente();
            if (agregarCliente.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cliente Clientenuevo = new Cliente()
                    {
                        ID = agregarCliente.Datos.ID,
                        Nombre = agregarCliente.Datos.Nombre,
                        Direccion = agregarCliente.Datos.Direccion,
                        Correo = agregarCliente.Datos.Correo,
                        Tipo = agregarCliente.Datos.Tipo
                    };
                    
                    cliente.Add(Clientenuevo);
                    dataGridView1.Rows.Add(Clientenuevo.ID, Clientenuevo.Nombre, Clientenuevo.Direccion, Clientenuevo.Correo, Clientenuevo.Tipo);

                }
                catch
                {
                    MessageBox.Show("Error al ingresar los datos. Verifique que haya ingresado correctamente los campos");
                }

            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
           
        }
    }
}
