using System.Globalization;
using System.Runtime.Versioning;
using System.Text.Json;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    [SupportedOSPlatform("windows6.1")]
    public partial class Inventario : Form
    {
        //revisar cuando se elimina, buscar en datagrid, 
        private Producto Producto;
        private List<Producto> inventario = new List<Producto>();
        RegistroPedidos.Usuarios Usuarioactual = new RegistroPedidos.Usuarios();
        public Inventario(Usuarios usuarioactual)
        {
            InitializeComponent();
            Producto = new Producto();
            dataGridView1.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["PrecioUnitario"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
            CargarInventario("Inventario.Json");
            Usuarioactual = usuarioactual;
            ControlUsuario1(usuarioactual);
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Codigo_especial Codigo = new Codigo_especial();
            if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")
            {
                Codigo.ShowDialog();
                if (Codigo.DialogResult == DialogResult.OK)
                {
                    casillaSeleccionada(e);

                }
            }
            else casillaSeleccionada(e);

        }
        public void casillaSeleccionada(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                Agregar_Productos formEditar = new Agregar_Productos();
                formEditar.SetDatosProducto(
                    filaSeleccionada.Cells["ID"].Value.ToString() ?? "0",
                    filaSeleccionada.Cells["Nombre"].Value.ToString() ?? "",
                    filaSeleccionada.Cells["Categoria"].Value.ToString() ?? "",

                    filaSeleccionada.Cells["Stock"].Value.ToString() ?? "",
                     filaSeleccionada.Cells["Descripcion"].Value.ToString() ?? "",
                    filaSeleccionada.Cells["PrecioUnitario"].Value.ToString() ?? ""
                );

                formEditar.ShowDialog();
                if (formEditar.DialogResult == DialogResult.OK)
                {
                    DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                    Producto productoEditado = formEditar.ObtenerProductoEditado();
                    fila.Cells["Nombre"].Value = productoEditado.Nombre;
                    fila.Cells["Categoria"].Value = productoEditado.Categoria;
                    fila.Cells["Descripcion"].Value = productoEditado.Descripcion;
                    fila.Cells["Stock"].Value = productoEditado.Cantidad;

                    fila.Cells["PrecioUnitario"].Value = productoEditado.PrecioUnitario.ToString(); ;
                    GuardarInventario("Inventario.Json");
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    GuardarInventario("Inventario.Json");
                }
            }



        }

        public void GuardarInventario(string rutaArchivo)
        {
            List<Producto> listaProductos = new List<Producto>();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["ID"].Value != null) // Validamos que la fila tenga datos
                {
                    Producto producto = new Producto();
                    try
                    {
                        // Fix for CS0019 and CS8604 in the problematic line
                        producto.ID = int.TryParse(fila.Cells["ID"].Value?.ToString(), out int id) ? id : 0;
                        producto.Nombre = fila.Cells["Nombre"].Value.ToString() ?? "";
                        producto.Categoria = fila.Cells["Categoria"].Value.ToString() ?? "";
                        producto.Descripcion = fila.Cells["Descripcion"].Value.ToString() ?? "";
                        producto.Cantidad = int.TryParse(fila.Cells["Stock"].Value.ToString(), out int Cantidad) ? Cantidad : 0;
                        if (fila.Cells["PrecioUnitario"].Value != null)
                        {
                            producto.PrecioUnitario = decimal.Parse(fila.Cells["PrecioUnitario"].Value.ToString());
                        }
                        else
                        {
                            producto.PrecioUnitario = 0; // Or handle the default value appropriately

                        }
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show($"Error de formato: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                    listaProductos.Insert(0, producto); // Agregamos al inicio para mantener el orden
                }
            }

            string json = JsonSerializer.Serialize(listaProductos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaArchivo, json);
            CargarInventario("Inventario.Json");
        }
        public void EliminarProducto(int id)
        {
            Producto producto = inventario.Find(p => p.ID == id);
            if (producto != null)
                inventario.Remove(producto);

        }
        [SupportedOSPlatform("windows6.1")]

        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 4 caracteres
            if (textoBusqueda.Length >= 3)
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
        [SupportedOSPlatform("windows6.1")]
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
        [SupportedOSPlatform("windows6.1")]
        public void ExportarCSV(string rutaArchivo)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaArchivo))
                {
                    sw.WriteLine("ID,Nombre,Categoria,Descripcion,PrecioUnitario"); // Encabezado CSV

                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        if (fila.Cells["ID"].Value != null)
                        {
                            sw.WriteLine($"{fila.Cells["ID"].Value},{fila.Cells["Nombre"].Value},{fila.Cells["Categoria"].Value},{fila.Cells["Descripcion"].Value},{fila.Cells["Stock"].Value},{fila.Cells["PrecioUnitario"].Value}");
                        }
                    }
                }

                MessageBox.Show("Exportación realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        [SupportedOSPlatform("windows6.1")]
        public void ImportarCSV(string rutaArchivo)
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    var lineas = File.ReadAllLines(rutaArchivo);
                    dataGridView1.Rows.Clear();

                    foreach (var linea in lineas.Skip(1)) // Omitimos el encabezado
                    {
                        var datos = linea.Split(',');

                        dataGridView1.Rows.Add(datos[0], datos[1], datos[2], datos[3], datos[4], decimal.Parse(datos[5]));
                    }

                    MessageBox.Show("Importación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GuardarInventario("Inventario.Json");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            BuscarElemento(guna2TextBox2.Text);
        }
        [SupportedOSPlatform("windows6.1")]
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            {

                sfd.Filter = "Archivos CSV (*.csv)|*.csv";
                sfd.Title = "Selecciona dónde guardar el archivo CSV";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportarCSV(sfd.FileName);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Agregar_Productos formProductos = new Agregar_Productos();
            if (formProductos.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Producto ProductoNuevo = new Producto
                    {
                        ID = formProductos.Id,
                        Nombre = formProductos.Nombre,
                        Categoria = formProductos.Categoria,
                        Descripcion = formProductos.Descripcion,
                        PrecioUnitario = formProductos.PrecioUnitario,
                        Cantidad = formProductos.Cantidad
                    };

                    inventario.Add(ProductoNuevo);
                    dataGridView1.Rows.Add(ProductoNuevo.ID, ProductoNuevo.Nombre, ProductoNuevo.Categoria, ProductoNuevo.Descripcion, ProductoNuevo.Cantidad, ProductoNuevo.PrecioUnitario);
                    GuardarInventario("Inventario.Json");
                }
                catch
                {
                    MessageBox.Show("Error al ingresar los datos. Verifique que haya ingresado correctamente los datos");
                }

            }
            else
            {
                //nada xd
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos CSV (*.csv)|*.csv";
                ofd.Title = "Selecciona un archivo CSV para importar";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ImportarCSV(ofd.FileName);
                }
            }
        }

      
        public void ControlUsuario1(RegistroPedidos.Usuarios Usuarioactual)
        {

            if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        // Existing code...
    }
}
