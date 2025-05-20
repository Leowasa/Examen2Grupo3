using System.Runtime.Versioning;
using System.Text.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    [SupportedOSPlatform("windows6.1")]
    public partial class Inventario : Form
    {
        //revisar cuando se elimina, buscar en datagrid, 
        private List<Producto> inventario = new List<Producto>();
        Datos.Usuarios Usuarioactual = new Datos.Usuarios();
        [SupportedOSPlatform("windows6.1")]
        public Inventario(Usuarios usuarioactual)
        {
            InitializeComponent();
            dataGridView1.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["PrecioUnitario"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
            CargarInventario("Inventario.Json");
            Usuarioactual = usuarioactual;
            ControlUsuario1(usuarioactual);//restringo las funciones mostradas al usuario ingresado
            tootip();
        }

        private void tootip()
        {
            ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
            toolTip1.SetToolTip(pictureBox1, "Importar.");
            ToolTip tooltip2 = new ToolTip();
            tooltip2.SetToolTip(pictureBox2, "Exportar.");

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Codigo_especial Codigo = new Codigo_especial();
            if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index && e.RowIndex >= 0)
            {

                if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")//si es aprobador o registrador. Se le solicita clave especial
                {
                    Codigo.ShowDialog();
                    if (Codigo.DialogResult == DialogResult.OK)//si se ingreso correctamente
                    {
                        //proceder con el resto del codigo

                    }
                    else return;//cancelar la operacion si la operacion no fue fallida
                }
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                Agregar_Productos formEditar = new Agregar_Productos(inventario, 1);//obtengo los datos del productos desde la fila seleccionada y opero la edicion
                formEditar.SetDatosProducto(
                    filaSeleccionada.Cells["ID"].Value.ToString() ?? "0",
                    filaSeleccionada.Cells["Nombre"].Value.ToString() ?? "",
                    filaSeleccionada.Cells["Categoria"].Value.ToString() ?? "",

                    filaSeleccionada.Cells["Stock"].Value.ToString() ?? "",
                     filaSeleccionada.Cells["Descripcion"].Value.ToString() ?? "",
                    filaSeleccionada.Cells["PrecioUnitario"].Value.ToString() ?? ""
                );

                formEditar.ShowDialog();
                if (formEditar.DialogResult == DialogResult.OK)//si la operacion fue exitosa. Cargar los datos y despues guardar
                {
                    DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                    Producto productoEditado = formEditar.ObtenerProductoEditado();
                    inventario[e.RowIndex] = productoEditado;
                    GuardarInventario("Inventario.Json");
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {

                if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")//si es aprobador o registrador. Se le solicita clave especial
                {
                    Codigo.ShowDialog();
                    if (Codigo.DialogResult == DialogResult.OK)//si se ingreso correctamente
                    {
                        //proceder con el resto del codigo

                    }
                    else return;//cancelar la operacion si la operacion no fue fallida
                }
                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    inventario.RemoveAt(e.RowIndex);//elimino el producto de la lista y luego guardo
                    GuardarInventario("Inventario.Json");
                }
            }


        }

        public void casillaSeleccionada(DataGridViewCellEventArgs e)//se obtiene la casilla seleccionada y realiza la operacion correspondiente
        {



        }

        public void GuardarInventario(string rutaArchivo)
        {


            if (inventario != null) // Validamos que la fila tenga datos
            {
                string json = JsonSerializer.Serialize(inventario, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(rutaArchivo, json);//serializo, lo escribo en el json y luego cargo los cambios
                CargarInventario("Inventario.Json");
            }
            else MessageBox.Show("Error al guardar los datos. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }
        public void EliminarProducto(int id)
        {
            Producto producto = inventario.Find(p => p.ID == id);
            if (producto != null)
                inventario.Remove(producto);

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
                bool coincide = (fila.Cells["ID"].Value != null && fila.Cells["ID"].Value.ToString().Contains(filtro, StringComparison.CurrentCultureIgnoreCase)) ||
                                (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }
        public void CargarInventario(string rutaArchivo)
        {

            if (File.Exists(rutaArchivo))
            {

                string json = File.ReadAllText(rutaArchivo);
                inventario = JsonSerializer.Deserialize<List<Producto>>(json) ?? new List<Producto>();

                if (inventario != null) // Verificar que la lista no sea nula
                {
                    dataGridView1.Rows.Clear(); // Limpiar la tabla antes de cargar nuevos datos

                    foreach (var producto in inventario)
                    {
                        dataGridView1.Rows.Add(producto.ID, producto.Nombre, producto.Categoria, producto.Descripcion, producto.Cantidad, producto.PrecioUnitario);
                    }
                }
            }
        }
        public void ExportarCSV(string rutaArchivo)
        {
            try
            {
                // Crea un StreamWriter para escribir en el archivo CSV especificado por rutaArchivo
                using (StreamWriter sw = new StreamWriter(rutaArchivo))
                {
                    // Escribe la primera línea del archivo, que corresponde al encabezado de las columnas
                    sw.WriteLine("ID,Nombre,Categoria,Descripcion,PrecioUnitario");

                    // Recorre todas las filas del datagrid
                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        // Verifica que la celda "ID" no sea nula (evita filas vacías)
                        if (fila.Cells["ID"].Value != null)
                        {
                            // Escribe una línea en el archivo CSV con los valores de la fila, separados por comas
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
        public void ImportarCSV(string rutaArchivo)
        {
            // Inicializa la lista de inventario como una nueva lista vacía
            inventario = new List<Producto>();

            try
            {
                // Verifica si el archivo especificado existe
                if (File.Exists(rutaArchivo))
                {
                    // Lee todas las líneas del archivo CSV
                    var lineas = File.ReadAllLines(rutaArchivo);

                    // Recorre cada línea, omitiendo la primera (encabezado)
                    foreach (var linea in lineas.Skip(1))
                    {
                        // Divide la línea en partes usando la coma como separador
                        var datos = linea.Split(',');

                        // Crea un nuevo objeto Producto y asigna los valores leídos del CSV
                        Producto producto = new Producto
                        {
                            ID = int.Parse(datos[0]),
                            Nombre = datos[1],
                            Categoria = datos[2],
                            Descripcion = datos[3],
                            Cantidad = int.Parse(datos[4]),
                            PrecioUnitario = decimal.Parse(datos[5])
                        };

                        // Agrega el producto a la lista de inventario
                        inventario.Add(producto);
                    }


                    MessageBox.Show("Importación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    GuardarInventario("Inventario.Json");


                    CargarInventario("Inventario.Json");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2TextBox2_TextChanged(object sender, EventArgs e)//barra de busqueda
        {
            BuscarElemento(guna2TextBox2.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)//btn para importar
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
        private void pictureBox2_Click(object sender, EventArgs e)//btn para exportar
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

        private void guna2Button2_Click(object sender, EventArgs e)//btn para agregar producto
        {
            Agregar_Productos formProductos = new Agregar_Productos(inventario, 2);
            if (formProductos.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Producto ProductoNuevo = new Producto
                    {
                        ID = formProductos.producto.ID,
                        Nombre = formProductos.producto.Nombre,
                        Categoria = formProductos.producto.Categoria,
                        Descripcion = formProductos.producto.Descripcion,
                        PrecioUnitario = formProductos.producto.PrecioUnitario,
                        Cantidad = formProductos.producto.Cantidad
                    };

                    inventario.Add(ProductoNuevo);
                    dataGridView1.Rows.Add(ProductoNuevo.ID, ProductoNuevo.Nombre, ProductoNuevo.Categoria, ProductoNuevo.Descripcion, ProductoNuevo.Cantidad, ProductoNuevo.PrecioUnitario);
                    GuardarInventario("Inventario.Json");
                }
                catch
                {
                    MessageBox.Show("Error al ingresar los datos. Verifique que haya ingresado correctamente los Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }




        public void ControlUsuario1(Datos.Usuarios Usuarioactual)//oculta los botones de importar y exportar si es Aprobador o Registrador
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

    }
}
