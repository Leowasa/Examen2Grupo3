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
            CargarClientes("Clientes.Json");
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtén los datos del cliente desde la fila seleccionada
                Clientes.ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                Clientes.Nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                Clientes.Correo = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                Clientes.Direccion = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                // Dispara el evento enviando el cliente seleccionado
                //ClienteSeleccionado?.Invoke(clienteSeleccionado);
                ClienteSeleccionado?.Invoke(Clientes);

                // Cierra el formulario
                this.Close();
            }
        }
        private Cliente ObtenerClienteDesdeFila(int rowIndex)
        {
            return new Cliente
            {
                Nombre = dataGridView1.Rows[rowIndex].Cells["Nombre"].Value?.ToString() ?? "",
                ID = int.Parse(dataGridView1.Rows[rowIndex].Cells["ID"].Value?.ToString() ?? "0"),
                Direccion = dataGridView1.Rows[rowIndex].Cells["Direccion"].Value?.ToString() ?? "",
                Correo = dataGridView1.Rows[rowIndex].Cells["Correo"].Value?.ToString() ?? "",
                Tipo = dataGridView1.Rows[rowIndex].Cells["Tipo"].Value?.ToString() ?? ""
            };
        }

        private void BuscarClientes_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
