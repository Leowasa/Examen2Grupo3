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

        // Fix for CS1503: Use JsonConvert from Newtonsoft.Json instead of JsonSerializer
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


        // Actualizar el DataGridView con la lista de pedidow
        private void ActualizarDataGridView()
        {

            dataGridView1.Rows.Clear();
            foreach (var datos in listaPedido)
            {
                string nombreCliente = datos.Cliente?.Nombre ?? "Desconocido";
                string fechaCreacion = datos.Fecha != null ? datos.Fecha.ToString("dd/MM/yyyy") : "Desconocida";

                dataGridView1.Rows.Add(
                datos.ID,               // Número de Pedido
                nombreCliente,           // Nombre del Cliente
                fechaCreacion,           // Fecha de creación
                datos.Total.ToString("C2"), // Total formateado como moneda
                datos.Estado             // Estado del pedido


                 );

            }

        }

        // Botón para agregar un nuevo pedido
        private void btnAgregarPedido_Click(object sender, EventArgs e)
        {
            // Simulación de agregar un pedido (Aquí puedes abrir un formulario o ingresar datos manualmente)
            Pedido nuevoPedido = new Pedido
            {
                ID = listaPedido.Count + 1,
                Cliente = new Cliente { Nombre = "Nuevo Cliente" },
                Fecha = DateTime.Now,
                Total = 100.0m,
                Estado = "En proceso"
            };

            listaPedido.Add(nuevoPedido);
            ActualizarDataGridView();
            GuardarDatosJson();
        }

        // Botón para editar un pedido
        private void btnEditarPedido_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idPedido = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                Pedido pedidoSeleccionado = listaPedido.Find(p => p.ID == idPedido);

                if (pedidoSeleccionado != null)
                {
                    // Modificar estado del pedido
                    pedidoSeleccionado.Estado = "Entregado";  // Cambiar según lo que necesites

                    // Actualizar en el DataGridView
                    ActualizarDataGridView();
                    GuardarDatosJson();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un pedido para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        // Botón para eliminar un pedido
        private void btnEliminarPedido_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idPedido = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                Pedido pedidoAEliminar = listaPedido.Find(p => p.ID == idPedido);

                if (pedidoAEliminar != null)
                {
                    listaPedido.Remove(pedidoAEliminar);
                    ActualizarDataGridView();
                    GuardarDatosJson();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un pedido para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            CargarDatosDesdeJson();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


