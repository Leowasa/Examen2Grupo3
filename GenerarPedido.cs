using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using static Examen2Grupo3.GenerarPedido;

namespace Examen2Grupo3
{
    public partial class GenerarPedido : Form
    {
        public class Pedido
        {
            public DateTime Fecha { get; set; }
            public string? NombreCliente { get; set; }
            public int? IDCliente { get; set; }
            public string? Direccion { get; set; }
            public string? Correo { get; set; }
            public List<Producto>? Productos { get; set; }
            public decimal Total { get; set; }
        }

        public class Producto
        {
            public int ID { get; set; }
            public string? Nombre { get; set; }
            public string? Descripcion { get; set; }
            public string? Categoria { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Total => Cantidad * PrecioUnitario;
        }
        private List<Pedido> historialPedidos;
        private List<Producto>? productos; // Lista de productos para el DataGridView

        public GenerarPedido(List<Pedido> historial)
        {
            InitializeComponent();
            historialPedidos = historial;
            dataGridView1.AutoGenerateColumns = true; // Activa la generación automática de columnas

        }
        public class RoundButton : Button
        {
            protected override void OnPaint(PaintEventArgs pevent)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, Width, Height);
                this.Region = new Region(path);
                base.OnPaint(pevent);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Crear un nuevo pedido
            Pedido nuevoPedido = new Pedido
            {
                Fecha = DateTime.Now,
                NombreCliente = guna2TextBox1.Text,
                IDCliente = int.Parse(guna2TextBox2.Text),
                Direccion = guna2TextBox3.Text,
                Correo = guna2TextBox4.Text,
                Productos = productos, // Usar la lista de productos actual
                Total = CalcularTotal()
            };

            // Agregar el pedido al historial
            historialPedidos.Add(nuevoPedido);

            // Mostrar un mensaje de confirmación
            MessageBox.Show("Pedido registrado correctamente.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in productos)
            {
                total += producto.Total;
            }
            return total;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario Agregar_Productos y pasarle la lista de productos y el formulario actual
            Agregar_Productos ventana = new Agregar_Productos(productos, this);

            // Mostrar la ventana emergente como modal
            if (ventana.ShowDialog() == DialogResult.OK)
            {
                // Actualizar el DataGridView con los productos agregados
                ActualizarDataGridView();
            }
        }


        public void ActualizarDataGridView()
        {
            if (productos == null)
            {
                productos = new List<Producto>(); // Inicializar la lista si es null
            }

            dataGridView1.AutoGenerateColumns = true; // Asegurar que las columnas se generen automáticamente
            dataGridView1.DataSource = null; // Desvincular la fuente de datos actual
            dataGridView1.DataSource = productos; // Volver a vincular la lista actualizada
            dataGridView1.Refresh(); // Refrescar el DataGridView
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
