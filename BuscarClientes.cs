using System.Runtime.InteropServices;
using System.Text.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class BuscarClientes : Form
    {
        public List<Cliente>? cliente = new List<Cliente>();
        public event Action<Cliente> ClienteSeleccionado;
        public Cliente Clientes = new Cliente();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        public BuscarClientes()
        {
            InitializeComponent();
            CargarClientes("Clientes.Json");//Carga el cliente y lo actualiza en el datagridvew
        }
        public void CargarClientes(string rutaArchivo)
        {

            if (File.Exists(rutaArchivo))
            {

                var json = File.ReadAllText(rutaArchivo);
                List<Cliente>? listaProductos = JsonSerializer.Deserialize<List<Cliente>>(json);

                if (listaProductos != null) // Verificar que la lista no sea nula
                {
                    dataGridView1.Rows.Clear(); // Limpiar la tabla antes de cargar nuevos datos

                    foreach (var producto in listaProductos)
                    {
                        dataGridView1.Rows.Add(producto.ID, producto.Nombre, producto.Correo, producto.Direccion, producto.Tipo);
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//Cargar el Cliente de la fila seleccionada
        {
            if (e.RowIndex >= 0)
            {
                // Validar que las celdas no sean nulas antes de convertirlas  
                var idValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
                var nombreValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                var correoValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
                var direccionValue = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();

                if (!string.IsNullOrEmpty(idValue) &&
                    !string.IsNullOrEmpty(nombreValue) &&
                    !string.IsNullOrEmpty(correoValue) &&
                    !string.IsNullOrEmpty(direccionValue))
                {
                    Clientes.ID = int.Parse(idValue);
                    Clientes.Nombre = nombreValue;
                    Clientes.Correo = correoValue;
                    Clientes.Direccion = direccionValue;

                    // Dispara el evento enviando el cliente seleccionado  
                    ClienteSeleccionado?.Invoke(Clientes);

                    // Cierra el formulario  
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Algunos datos del cliente están incompletos o son nulos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BuscarClientes_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)//btn cerrrar
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)//barra de busqueda
        {
            BuscarElemento(guna2TextBox2.Text);
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
                bool coincide = (fila.Cells["ID"].Value != null && fila.Cells["ID"].Value.ToString().ToLower().Contains(filtro)) ||
                                (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }
    }
}
