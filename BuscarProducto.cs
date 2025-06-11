using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using ejemplo;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class BuscarProducto : Form
    {
        public Producto Producto = new Producto();
        private List<Producto> listaProductos = new List<Producto>();
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        private BindingSource bindingSource = new BindingSource();
        public BuscarProducto()
        {
            InitializeComponent();
            dataGridView1.Columns["PrecioUnit"].DefaultCellStyle.Format = "C2";//Permite que la columna muestre decimales
            dataGridView1.Columns["PrecioUnit"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");//Muestra el precio en $
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
            CargarInventario("Inventario.json");
        }
        public void CargarInventario(string rutaArchivo)
        {

            if (File.Exists(rutaArchivo))
            {


                string json = File.ReadAllText(rutaArchivo);
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(json) ?? new List<Producto>();
                bindingSource.DataSource = listaProductos;
                bindingSource.ResetBindings(false);
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
            // Verificar que el texto de búsqueda tenga al menos 3 caracteres
            if ((string.IsNullOrWhiteSpace(textoBusqueda) || textoBusqueda.Length < 3))
            {
                // Mostrar todos los productos
                bindingSource.DataSource = new List<Producto>(listaProductos);

            }
            else
            {
                string filtro = textoBusqueda.ToLower();
                var filtrados = listaProductos.Where(p =>
                    p.ID.ToString().Contains(filtro) ||
                    (p.Nombre != null && p.Nombre.ToLower().Contains(filtro)) ||
                    (p.IDbarra != null && p.IDbarra.ToLower().Contains(filtro))
                ).ToList();

                bindingSource.DataSource = filtrados;
            }
            bindingSource.ResetBindings(false);
        }
        private void guna2TextBox7_TextChanged(object sender, EventArgs e)//barra de busqueda
        {
            BuscarElemento(guna2TextBox7.Text);
        }

      

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//obtener el producto seleccionado
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
      
        private void pictureBox2_Click(object sender, EventArgs e)//btn cerrar
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)//para mover el formulario
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Producto = new Producto();
            try
            {
                Producto.ID = guna2TextBox1.Text;
                Producto.Nombre = guna2TextBox2.Text;
                Producto.Categoria = guna2TextBox4.Text;
                Producto.Descripcion = guna2TextBox3.Text;
                Producto.Cantidad = int.Parse(guna2TextBox5.Text);
                Producto.PrecioUnitario = Decimal.Parse(guna2TextBox6.Text);
                if (Validar(Producto) == false)
                {
                    MessageBox.Show("La cantidad ingresada excede el stock disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (OverflowException)//Cantidad ingresada que exceda el tamaño soportado por el int
            {
                MessageBox.Show("El número ingresado es demasiado grande o pequeño para este tipo de dato.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch
            {
                MessageBox.Show("Campos vacios o erroneos. Intente nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
