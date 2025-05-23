using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class BuscarClientes : Form
    {
        public event Action<Cliente> ClienteSeleccionado;
        public Cliente Clientes= new Cliente();
        List<Cliente> listaClientes = new List<Cliente>();
        private BindingSource bindingSource = new BindingSource();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        public BuscarClientes()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
            CargarClientes("Clientes.Json");//Carga el cliente y lo actualiza en el datagridvew
        }
        public void CargarClientes(string rutaArchivo)
        {

            if (File.Exists(rutaArchivo))
            {

                var json = File.ReadAllText(rutaArchivo);
                listaClientes = JsonSerializer.Deserialize<List<Cliente>>(json)?? new List<Cliente>();
                bindingSource.DataSource = listaClientes;
                bindingSource.ResetBindings(false);
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

            // Verificar que el texto de búsqueda tenga al menos 3 caracteres
            if ((string.IsNullOrWhiteSpace(textoBusqueda) || textoBusqueda.Length < 3))
            {
                // Mostrar todos los Clientes
                bindingSource.DataSource = new List<Cliente>(listaClientes);

            }
            else
            {
                string filtro = textoBusqueda.ToLower();
                var filtrados = listaClientes.Where(p =>
                    p.ID.ToString().Contains(filtro) ||
                    (p.Nombre != null && p.Nombre.ToLower().Contains(filtro))
                ).ToList();

                bindingSource.DataSource = filtrados;
            }
            bindingSource.ResetBindings(false);
        }
    }
}
