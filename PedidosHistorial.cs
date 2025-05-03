using System.IO; // Ensure this is included for File operations
using System.Windows.Forms;
using Examen2Grupo3;
using Newtonsoft.Json;
using System.Collections.Generic; // Ensure this is included for List<T>
using static Examen2Grupo3.RegistroPedidos;
using System.Drawing.Text;


namespace ejemplo
{

    public partial class PedidosHistorial : Form
    {
        private List<Producto> listaPersonas = new List<Producto>();

        private List<Pedido> listaPedido = new List<Pedido>();
        public PedidosHistorial()
        {
            InitializeComponent();
            CargarDatosDesdeJson();
        }


        private void CargarDatosDesdeJson()
        {
            string rutaArchivo = "datos.json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonString);

                if (pedidos != null && pedidos.Count > 0)
                {
                    foreach (var datos in pedidos)
                    {
                        string nombreCliente = datos.Cliente?.Nombre ?? "Desconocido";

                        // Aseguramos que la fecha no sea nula y la mostramos correctamente
                        string fechaCreacion = datos.Fecha != null ? datos.Fecha.ToString("dd/MM/yyyy") : "Desconocida";

                        // Añadimos la fila correspondiente al DataGridView
                        dataGridView1.Rows.Add(
                            datos.ID,               // Número de Pedido
                            nombreCliente,           // Nombre del Cliente
                            fechaCreacion,           // Fecha de creación
                            datos.Total.ToString("C2"), // Total formateado como moneda
                            datos.Estado             // Estado del pedido
                        );

                    }
                }

                else
                {
                    MessageBox.Show("No se encontraron pedidos.", "Historial de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else
            {
                MessageBox.Show("El archivo de datos no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Actualizar el DataGridView con la lista de pedido
        private void ActualizarDataGridView()
        {
            dataGridView1.Rows.Clear();

            if (listaPedido == null || listaPedido.Count == 0)
                return;

            decimal totalConDescuento = 0;
            int cantidadProductos = 0;

            foreach (var datos in listaPedido)
            {
                string nombreCliente = datos.Cliente?.Nombre ?? "Desconocido";
                string fechaCreacion = datos.Fecha != null ? datos.Fecha.ToString("dd/MM/yyyy") : "Desconocida";

                cantidadProductos += datos.Productos?.Count ?? 0;
                totalConDescuento += MontoTotal(datos);

                dataGridView1.Rows.Add(
                    datos.ID,
                    nombreCliente,
                    fechaCreacion,
                    datos.Total.ToString("C2"),
                    MontoTotal(datos).ToString("C2"),
                    datos.Estado,
                    datos.Productos.Count > 3 ? "20%" : "0%"  // Nueva columna de descuento
                );
            }

            // Mostrar el descuento aplicado y el total final
            MessageBox.Show($"Descuento aplicado: {(cantidadProductos > 3 ? "20%" : "0%")}\nTotal final: {totalConDescuento.ToString("C")}",
                            "Resumen de Pedido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);


        }


        // Me permite actualizar el estado del pedido
        private void CambiarEstadoPedido(int idPedido, string nuevoEstado)
        {

            List<string> estadosValidos = new List<string> { "En proceso", "Entregado", "Cancelado" };

            // Validar que el nuevo estado sea válido
            if (!estadosValidos.Contains(nuevoEstado))
            {
                MessageBox.Show($"El estado '{nuevoEstado}' no es válido. Los estados permitidos son: {string.Join(", ", estadosValidos)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Buscar el pedido con el ID indicado
            Pedido pedidoSeleccionado = listaPedido.Find(p => p.ID == idPedido);

            if (pedidoSeleccionado != null)
            {
                // Cambiar el estado del pedido
                pedidoSeleccionado.Estado = nuevoEstado;
                ActualizarDataGridView();
                GuardarDatosJson();

                MessageBox.Show($"El estado del pedido con ID {idPedido} ha sido cambiado a '{nuevoEstado}'.", "Estado Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"No se encontró ningún pedido con ID {idPedido}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Metodo para calcular el monto total de la proforma

        private decimal MontoTotal(Pedido pedido)
        {
            if (pedido?.Productos == null || pedido.Productos.Count == 0)
            {
                return 0;
            }

            decimal subtotal = pedido.SubtTotal;
            decimal descuento = pedido.Productos.Count > 3 ? subtotal * 0.20m : 0;

            decimal totaldescuento = subtotal - descuento;

            return totaldescuento;

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
            CargarDatosDesdeJson();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       

        //Botones en DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si el usuario hizo clic en el botón "Editar"
            if (e.ColumnIndex == dataGridView1.Columns["btnEditar"].Index && e.RowIndex >= 0)
            {
                int idPedido = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                // Pedir el nuevo estado al usuario
                string nuevoEstado = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo estado (En proceso, Entregado, Cancelado):",
                    "Editar Estado del Pedido", "En proceso");

                CambiarEstadoPedido(idPedido, nuevoEstado); // Llamar la función para cambiar el estado
            }

            // Si el usuario hizo clic en el botón "Eliminar"
            if (e.ColumnIndex == dataGridView1.Columns["btnEliminar"].Index && e.RowIndex >= 0)
            {
                int idPedido = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                // Confirmación antes de eliminar
                DialogResult resultado = MessageBox.Show($"¿Seguro que quieres eliminar el pedido {idPedido}?",
                    "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    // Buscar y eliminar el pedido de la lista
                    Pedido pedidoAEliminar = listaPedido.Find(p => p.ID == idPedido);
                    if (pedidoAEliminar != null)
                    {
                        listaPedido.Remove(pedidoAEliminar);

                        // Actualizar el DataGridView y guardar cambios
                        ActualizarDataGridView();
                        GuardarDatosJson();

                        MessageBox.Show($"Pedido {idPedido} eliminado correctamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }


        }
    }
}


