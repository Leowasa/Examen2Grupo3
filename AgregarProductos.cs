using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen2Grupo3
{
    public partial class Agregar_Productos : Form
    {
        private List<GenerarPedido.Producto> productos = new List<GenerarPedido.Producto>();
        private GenerarPedido parentForm;
        public Agregar_Productos(List<GenerarPedido.Producto> productos, GenerarPedido parentForm)
        {
            InitializeComponent();
            this.productos = productos ?? new List<GenerarPedido.Producto>();
            this.parentForm = parentForm ?? throw new ArgumentNullException(nameof(parentForm));}
        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo producto con los datos ingresados  
                var nuevoProducto = new GenerarPedido.Producto
                {
                    ID = int.Parse(guna2TextBox1.Text), 
                    Nombre = guna2TextBox2.Text,
                    Descripcion = guna2TextBox3.Text,
                    Categoria = guna2TextBox4.Text,
                    Cantidad = int.Parse(guna2TextBox5.Text),
                    PrecioUnitario = decimal.Parse(guna2TextBox4.Text)
                };

                // Agregar el producto a la lista  
                productos.Add(nuevoProducto);

                parentForm.ActualizarDataGridView();

                // Cerrar el formulario con resultado OK  
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
