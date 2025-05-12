using ejemplo;
using Newtonsoft.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class GenerarOrden : Form
    {
        public static Pedido seleccionado = new Pedido();
        public static List<Pedido> Lista = new List<Pedido>();
        public Datos.Usuarios UsuarioActual = new Datos.Usuarios();
        // private bool _cargandoDatos = false;
        Form1 form = new Form1();
        public GenerarOrden(Datos.Usuarios UsuarioActual)
        {
            this.UsuarioActual = UsuarioActual;
            InitializeComponent();
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";//para Mostrar el monto con decimales
            dataGridView1.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");//y mostrarlo en dolares
            CargarDatosDesdeJson();
        }
        public List<Pedido> LeerPedidos()
        {
            string rutaArchivo = "pedidos.json";
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(contenido) ?? new List<Pedido>();
            }
            return new List<Pedido>();
        }


        private void CargarDatosDesdeJson()
        {
            // _cargandoDatos = true;
            string rutaArchivoOriginal = "pedidos.json";


            // Verificar si el archivo original existe
            if (File.Exists(rutaArchivoOriginal))
            {

                var pedidos = LeerPedidos();

                if (pedidos != null) 
                {
                    dataGridView1.Rows.Clear();
                    foreach (var datos in pedidos)
                    {

                        // Agregar una nueva fila con los datos
                        int rowIndex = dataGridView1.Rows.Add(datos.ID.ToString("D6"), datos.Cliente.Nombre, datos.Fecha.ToString("dd/MM/yyyy"), datos.Total, datos.Estado);

                    }
                    Lista = pedidos;
                }
            }
            else
            {
                MessageBox.Show("El archivo 'datos.json' no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void GuardarCambios(List<Pedido> orden)
        {
            string rutaArchivo = "pedidos.json";

            try
            {
                // Cargar los pedidos existentes
                List<Pedido> PedidosExistentes = LeerPedidos();

                    // Si no existe, no lo agregues (opcional: puedes lanzar un error o manejarlo de otra forma)
                    MessageBox.Show("El pedido no existe en la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                

                // Serializar la lista actualizada de pedidos
                var json = JsonConvert.SerializeObject(PedidosExistentes, Formatting.Indented);

                // Escribir el JSON en el archivo
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GenerarOrden_Load(object sender, EventArgs e)
        {
            

        }

        private void Cliente_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        public void AbrirOtroFormulario(Pedido seleccionadot, int opcion)
        {
            Form1 principal = (Form1)Application.OpenForms["Form1"];
            if (principal != null)
            {
             
                principal.AbrirFormularioEnPanel(new Factura(seleccionadot, 1, UsuarioActual)); // Abre el formulario Factura Con los detalles del pedido
     


            }
        }
        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 3 caracteres
            if (textoBusqueda.Length < 3)
            {
                // Si tiene menos de 3 caracteres, mostrar todas las filas
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
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)//barra de busqueda
        {
            BuscarElemento(guna2TextBox2.Text);
        }

        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//celda seleccionada del datagrid
        {

            if (e.ColumnIndex == dataGridView1.Columns["Ver"].Index && e.RowIndex >= 0)
            {
                if (Lista[e.RowIndex] != null)
                {
                    AbrirOtroFormulario(Lista[e.RowIndex], 1);
                }

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)//solicitara una clave especial en caso de eliminar un pedido preventivo siendo aprobador o registrador
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
            else if (e.ColumnIndex == dataGridView1.Columns["Estado"].Index && e.RowIndex >= 0)
            {
                Cambiar_estado estado = new Cambiar_estado(Lista[e.RowIndex], UsuarioActual);
                estado.ShowDialog();
                CargarDatosDesdeJson();

            }
        }
        private void eliminar(DataGridViewCellEventArgs e)
        {
           //Preguntar si en verdad desea eliminar el pedido
            DialogResult result = MessageBox.Show("¿Deseas eliminar este pedido?", "Confirmar eliminación",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Lista.RemoveAt(e.RowIndex);//elimina el pedido preventivo seleccionado
                dataGridView1.Rows.RemoveAt(e.RowIndex); // Elimina la fila seleccionada del datagrid
                GuardarCambios(Lista);
            }
        }
        public void Casillaseleccionada(DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_EditModeChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

       
      
    }
}
