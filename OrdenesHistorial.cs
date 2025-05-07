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
using ejemplo;

using System.Text.Json;
using Newtonsoft.Json;

namespace Examen2Grupo3
{
    public partial class OrdenesHistorial : Form
    {
        List<Pedido> listaPedidos = new List<Pedido>();
        public OrdenesHistorial()
        {
            InitializeComponent();
            CargarOrdenes();
        }
        public void CargarOrdenes()
        {

            if (File.Exists("Ordenes.json"))
            {

                string json = File.ReadAllText("Ordenes.json");
               listaPedidos = System.Text.Json.JsonSerializer.Deserialize<List<Pedido>>(json)?? new List<Pedido>();

                if (listaPedidos != null) // Verificar que la lista no sea nula
                {
                    dataGridView1.Rows.Clear(); // Limpiar la tabla antes de cargar nuevos datos

                    foreach (var producto in listaPedidos)
                    {
                        dataGridView1.Rows.Add(producto.ID, producto.Cliente.Nombre, producto.Fecha.ToString("dd/MM/yy"), producto.Total, producto.Estado);
                    }
                }
            }
        }
        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 4 caracteres
            if (textoBusqueda.Length < 4)
            {
                // Si tiene menos de 4 caracteres, mostrar todas las filas
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    fila.Visible = true;
                }
                return;
            }

            // Convertir el texto de búsqueda a minúsculas para una comparación insensible a mayúsculas/minúsculas
            string filtro = textoBusqueda.ToLower();

            // Iterar sobre las filas del DataGridView
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                // Verificar si la celda de ID o Nombre contiene el texto de búsqueda
                bool coincide = (fila.Cells["Numero"].Value != null && fila.Cells["Numero"].Value.ToString().ToLower().Contains(filtro)) ||
                                (fila.Cells["Nombres"].Value != null && fila.Cells["Nombres"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }
        private void GuardarCambios(List<Pedido> orden)
        {
            string rutaArchivo = "Ordenes.json";

            try
            {
                // Serializar la lista actualizada de pedidos
                var json = JsonConvert.SerializeObject(orden, Formatting.Indented);

                // Escribir el JSON en el archivo
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            BuscarElemento(guna2TextBox2.Text);
        }
        public void AbrirOtroFormulario(Pedido seleccionado)
        {
            Form1 principal = (Form1)Application.OpenForms["Form1"];
            if (principal != null)
            {
                principal.AbrirFormularioEnPanel(new Factura(seleccionado, 1)); // Reemplaza con el formulario que desees abrir
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si el usuario hizo clic en el botón "Editar"
            if (e.ColumnIndex == dataGridView1.Columns["Ver"].Index && e.RowIndex >= 0)
            {
                int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value);
                foreach (var lista in listaPedidos)
                {
                    if (lista.ID == idPedido)
                        AbrirOtroFormulario(lista);
                }
            }

            // Si el usuario hizo clic en el botón "Eliminar"
            if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {

                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Obtener el ID del pedido desde la celda correspondiente
                int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value);

                // Buscar el pedido en la lista por su ID
                Pedido pedidoAEliminar = listaPedidos.Find(p => p.ID == idPedido);

                if (pedidoAEliminar != null)
                {
                    // Eliminar el pedido de la lista
                    listaPedidos.Remove(pedidoAEliminar);

                    // Eliminar la fila del DataGridView
                    dataGridView1.Rows.RemoveAt(e.RowIndex);

                    // Guardar los cambios en el archivo JSON
                    GuardarCambios(listaPedidos);

                    // MessageBox.Show($"El pedido con ID {idPedido} ha sido eliminado correctamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"No se encontró ningún pedido con ID {idPedido}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
    }

}
