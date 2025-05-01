using System.Text.Json;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    public partial class BuscarProducto : Form
    {
        public Producto Producto = new Producto();
        private List<Producto> inventario = new List<Producto>();
        public BuscarProducto()
        {
            InitializeComponent();
            CargarInventario("Inventario.json");
        }
        public void CargarInventario(string rutaArchivo)
        {

            if (File.Exists(rutaArchivo))
            {

                string json = File.ReadAllText(rutaArchivo);
                List<Producto>? listaProductos = JsonSerializer.Deserialize<List<Producto>>(json);

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

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            string criterio = guna2TextBox2.Text;

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["ID2"].Value != null && fila.Cells["Nombre"].Value != null)
                {
                    string? id = fila.Cells["ID"].Value?.ToString();
                    string? nombre = fila.Cells["Nombre"].Value?.ToString()?.ToLower();

                    fila.Visible = id.Contains(criterio) || nombre.Contains(criterio);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtén los datos del cliente desde la fila seleccionada
                guna2TextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                guna2TextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                guna2TextBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                guna2TextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                guna2TextBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();


                // Dispara el evento enviando el cliente seleccionado
                //ClienteSeleccionado?.Invoke(clienteSeleccionado);
                //ClienteSeleccionado?.Invoke(Clientes);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
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
