using System.Runtime.InteropServices;
using System.Text.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class BuscarProducto : Form
    {
        public Producto Producto = new Producto();
        private List<Producto> listaProductos = new List<Producto>();
        private List<Producto> inventarioOriginal = new List<Producto>();
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public BuscarProducto()
        {
            InitializeComponent();
            dataGridView1.Columns["PrecioUnit"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["PrecioUnit"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");

            CargarInventario("Inventario.json");
        }
        public void CargarInventario(string rutaArchivo)
        {

            if (File.Exists(rutaArchivo))
            {


                string json = File.ReadAllText(rutaArchivo);
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(json) ?? new List<Producto>();

                if (listaProductos != null) // Verificar que la lista no sea nula
                {
                    dataGridView1.Rows.Clear(); // Limpiar la tabla antes de cargar nuevos datos

                    foreach (var producto in listaProductos)
                    {
                        dataGridView1.Rows.Add(producto.ID, producto.Nombre, producto.Categoria, producto.Descripcion, producto.Cantidad, producto.PrecioUnitario);
                    }
                }
            }
        }
        public void GuardarInventario(string rutaArchivo)
        {
            if (File.Exists(rutaArchivo))
            {

                string json = JsonSerializer.Serialize(listaProductos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(rutaArchivo, json);
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
                bool coincide = (fila.Cells["ID2"].Value != null && fila.Cells["ID"].Value.ToString().ToLower().Contains(filtro)) ||
                                (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }
        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            BuscarElemento(guna2TextBox2.Text);
        }

      

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtén los datos del Producto desde la fila seleccionada  
                guna2TextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                guna2TextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                guna2TextBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                guna2TextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                // Convertir explícitamente el valor a decimal antes de formatearlo  
                decimal precioUnitario = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                guna2TextBox6.Text = precioUnitario.ToString("F2");
            }
        }
        private bool Validar(Producto producto)//validar si la cantidad ingresada es mayor al del stock disponible
        {
            foreach (var lista in listaProductos)
            {
                if (lista.ID == producto.ID)
                {
                    if (producto.Cantidad > lista.Cantidad)
                    {
                        return false;
                    }
                    lista.Cantidad -= producto.Cantidad;
                    GuardarInventario("Inventario.json");
                }
            }
            return true;
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Producto = new Producto();
            try
            {
                Producto.ID = int.Parse(guna2TextBox1.Text);
                Producto.Nombre = guna2TextBox2.Text;
                Producto.Categoria = guna2TextBox4.Text;
                Producto.Descripcion = guna2TextBox3.Text;
                Producto.Cantidad = int.Parse(guna2TextBox5.Text);
                Producto.PrecioUnitario = Decimal.Parse(guna2TextBox6.Text);
                if (Validar(Producto) == false)
                {
                    MessageBox.Show("La cantidad ingresada excede el stock disponible.");
                    return;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch
            {
                MessageBox.Show("Campos vacios o erroneos. Intente nuevamente");
                return;
            }

        }
    }
}
