using System.IO; // Ensure this is included for File operations
using System.Windows.Forms;
using Examen2Grupo3;
using Newtonsoft.Json;
using System.Collections.Generic; // Ensure this is included for List<T>
using static Examen2Grupo3.Datos;
using System.Drawing.Text;
using System.Diagnostics;


namespace ejemplo
{

    public partial class PedidosHistorial : Form
    {
        private static List<Pedido> listaPedido = new List<Pedido>();
        private Datos.Usuarios UsuarioActual = new Datos.Usuarios();
        public PedidosHistorial(Datos.Usuarios usuario)
        {
            InitializeComponent();
            UsuarioActual  = usuario;
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
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
                            datos.ID.ToString("D6"),               // Número de Pedido
                            nombreCliente,           // Nombre del Cliente
                            fechaCreacion,           // Fecha de creación
                            datos.Total,            // Total formateado como moneda
                            datos.Estado             // Estado del pedido
                        );

                    }
                   
                }
            }
        }


      


        private void GuardarCambios(List<Pedido> orden)
        {
            string rutaArchivo = "pedidos.json";//ruta del archivo

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


        private void PedidosHistorial_Load(object sender, EventArgs e)
        {
            
           
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)//barra de busqueda
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
                principal.AbrirFormulario(new Factura(seleccionado, 1,UsuarioActual)); // Reemplaza con el formulario que desees abrir
            }
        }
        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 4 caracteres
            if (textoBusqueda.Length < 3)
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
            // Si el usuario hizo clic en el botón "Ver"
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
                Codigo_especial Form = new Codigo_especial();
                if (UsuarioActual.Tipo == "Aprobador" || UsuarioActual.Tipo == "Registrador")
                {
                    Form.ShowDialog();
                    if (Form.DialogResult == DialogResult.OK)
                    {
                        eliminar(e);
                        return;
                    }
                }
                else eliminar(e); return;

            }
        }
        private void eliminar(DataGridViewCellEventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Deseas eliminar este pedido?", "Confirmar eliminación",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // Obtener el ID del pedido desde la celda correspondiente
            int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value);

            // Buscar el pedido en la lista por su ID
            Pedido pedidoAEliminar = listaPedido.Find(p => p.ID == idPedido);

            if (pedidoAEliminar != null && result == DialogResult.Yes)
            {
                // Eliminar el pedido de la lista
                listaPedido.Remove(pedidoAEliminar);

                // Eliminar la fila del DataGridView
                dataGridView1.Rows.RemoveAt(e.RowIndex);

                // Guardar los cambios en el archivo JSON
                GuardarCambios(listaPedido);

                // MessageBox.Show($"El pedido con ID {idPedido} ha sido eliminado correctamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}



/*public void AbrirFormularioEnPanel(Form formulario)
        {// Limpiar cualquier formulario existente en el panel
            if (PanelPrincipal.Controls.Count > 0)
            {
                PanelPrincipal.Controls.RemoveAt(0);
            }

            // Configurar el formulario para que se adapte al panel
            formulario.TopLevel = false; // Indica que el formulario no es de nivel superior
            formulario.FormBorderStyle = FormBorderStyle.None; // Quitar bordes del formulario
            formulario.Dock = DockStyle.Fill; // Hacer que el formulario ocupe todo el panel

            // Agregar el formulario al panel y mostrarlo
            PanelPrincipal.Controls.Add(formulario);
            PanelPrincipal.Tag = formulario;
            formulario.Show();
        }
*/