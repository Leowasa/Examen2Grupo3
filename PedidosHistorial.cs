using System.IO; // Ensure this is included for File operations
using System.Windows.Forms;
using Examen2Grupo3;
using Newtonsoft.Json;
using System.Collections.Generic; // Ensure this is included for List<T>
using static Examen2Grupo3.RegistroPedidos;
using System.Drawing.Text;
using System.Diagnostics;


namespace ejemplo
{

    public partial class PedidosHistorial : Form
    {
        private List<Producto> listaPersonas = new List<Producto>();
        private static List<Pedido> listaPedido = new List<Pedido>();
        public PedidosHistorial()
        {
            InitializeComponent();
            CargarDatosDesdeJson();
        }

        // Fix for CS1503: Use JsonConvert from Newtonsoft.Json instead of JsonSerializer
        private void CargarDatosDesdeJson()
        {
            string rutaArchivo = "pedidos.json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                 listaPedido = JsonConvert.DeserializeObject<List<Pedido>>(jsonString)?? new List<Pedido>();
                dataGridView1.Rows.Clear();

                if (listaPedido != null && listaPedido.Count > 0)
                {
                    foreach (var datos in listaPedido)
                    {
                        string nombreCliente = datos.Cliente?.Nombre ?? "Desconocido";

                        // Aseguramos que la fecha no sea nula y la mostramos correctamente
                        string fechaCreacion = datos.Fecha != null ? datos.Fecha.ToString("dd/MM/yyyy") : "Desconocida";

                        // Añadimos la fila correspondiente al DataGridView
                        dataGridView1.Rows.Add(
                            datos.ID,               // Número de Pedido
                            nombreCliente,           // Nombre del Cliente
                            fechaCreacion,           // Fecha de creación
                            datos.Total,            // Total formateado como moneda
                            datos.Estado             // Estado del pedido
                        );

                    }
                   
                }
            }
        }


        // Actualizar el DataGridView con la lista de pedidow


        // Me permite actualizar el estado del pedido
        private void GuardarCambios(List<Pedido> orden)
        {
            string rutaArchivo = "pedidos.json";

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

        // Guardar los datos actualizados en el archivo JSON
        private void GuardarDatosJson()
        {
            string rutaArchivo = "datos.json";
            string jsonString = JsonConvert.SerializeObject(listaPedido, Formatting.Indented);
            File.WriteAllText(rutaArchivo, jsonString);
        }


        private void PedidosHistorial_Load(object sender, EventArgs e)
        {
            // Cargar los datos desde el archivo JSON al iniciar el formulario
           
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            BuscarElemento(guna2TextBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void AbrirOtroFormulario(Pedido seleccionado)
        {
            Form1 principal = (Form1)Application.OpenForms["Form1"];
            if (principal != null)
            {
                principal.AbrirFormularioEnPanel(new Factura(seleccionado, 1)); // Reemplaza con el formulario que desees abrir
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
                                (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }

        //Botones en DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si el usuario hizo clic en el botón "Editar"
            if (e.ColumnIndex == dataGridView1.Columns["Ver"].Index && e.RowIndex >= 0)
            {
                int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value);
                foreach (var lista in listaPedido)
                {
                    if (lista.ID == idPedido)
                        AbrirOtroFormulario(lista);
                }
            }

            // Si el usuario hizo clic en el botón "Eliminar"
            if (e.ColumnIndex == dataGridView1.Columns["btnEliminar"].Index && e.RowIndex >= 0)
            {

                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Obtener el ID del pedido desde la celda correspondiente
                int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value);

                // Buscar el pedido en la lista por su ID
                Pedido pedidoAEliminar = listaPedido.Find(p => p.ID == idPedido);

                if (pedidoAEliminar != null || result == DialogResult.Yes)
                {
                    // Eliminar el pedido de la lista
                    listaPedido.Remove(pedidoAEliminar);

                    // Eliminar la fila del DataGridView
                    dataGridView1.Rows.RemoveAt(e.RowIndex);

                    // Guardar los cambios en el archivo JSON
                    GuardarCambios(listaPedido);

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



