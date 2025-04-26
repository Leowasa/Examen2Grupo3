using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.RegistroPedidos;
using static Examen2Grupo3.GestorInventario;
using System.Text.Json;
using Examen2Grupo3;

namespace ejemplo
{
    public partial class Inventario : Form
    {
        private Producto Producto;
        private List<Producto> inventario = new List<Producto>();
        public Inventario()
        {
            InitializeComponent();
            GuardarInventario("Inventario.Json");
        }

        private void Inventario_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que la columna seleccionada sea la de "Editar"
            if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                // Creamos una instancia del formulario de edición y pasamos los datos
                Agregar_Productos formEditar = new Agregar_Productos();
                formEditar.SetDatosProducto(
                    filaSeleccionada.Cells["ID"].Value.ToString(),
                    filaSeleccionada.Cells["Nombre"].Value.ToString(),
                    filaSeleccionada.Cells["Categoria"].Value.ToString(),
                    filaSeleccionada.Cells["Descripcion"].Value.ToString(),
                     filaSeleccionada.Cells["Stock"].Value.ToString(),
                    filaSeleccionada.Cells["PrecioUnitario"].Value.ToString()
                );

                formEditar.ShowDialog(); // Mostramos el formulario de edición como una ventana modal
                if (formEditar.DialogResult == DialogResult.OK)
                {
                    DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                    Producto productoEditado = formEditar.ObtenerProductoEditado();
                    fila.Cells["Nombre"].Value = productoEditado.Nombre;
                    fila.Cells["Categoria"].Value = productoEditado.Categoria;
                    fila.Cells["Descripcion"].Value = productoEditado.Descripcion;
                    fila.Cells["Stock"].Value = productoEditado.Descripcion;
                    fila.Cells["PrecioUnitario"].Value = productoEditado.PrecioUnitario;
                    GuardarInventario("Inventario.Json");
                    CargarInventario("Inventario.Json");
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                // Verificar que la celda pertenece a la columna de botones y no es el encabezado

                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex); // Eliminar la fila seleccionada
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
                        producto.ID = int.Parse(fila.Cells["ID"].Value.ToString());
                        producto.Nombre = fila.Cells["Nombre"].Value.ToString();
                        producto.Categoria = fila.Cells["Categoria"].Value.ToString();
                        producto.Descripcion = fila.Cells["Descripcion"].Value.ToString();
                        producto.Cantidad = int.Parse(fila.Cells["Stock"].Value.ToString());
                        if (fila.Cells["PrecioUnitario"].Value != null)
                        {
                            producto.PrecioUnitario = decimal.Parse(fila.Cells["PrecioUnitario"].Value.ToString());
                        }
                        else
                        {
                            producto.PrecioUnitario = 0; // Or handle the default value appropriately

                        }
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

        private void guna2Button1_Click(object sender, EventArgs e)
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
                    //  listaProductos.Add(ProductoNuevo);
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
        public void EliminarProducto(int id)
        {
            Producto producto = inventario.Find(p => p.ID == id);
            if (producto != null)
                inventario.Remove(producto);
        }
        public void BuscarProducto()
        {
            string criterio = guna2TextBox2.Text;

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["ID"].Value != null && fila.Cells["Nombre"].Value != null)
                {
                    string? id = fila.Cells["ID"].Value?.ToString();
                    string nombre = fila.Cells["Nombre"].Value?.ToString()?.ToLower();

                    fila.Visible = id.Contains(criterio) || nombre.Contains(criterio);
                }
            }

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

                        dataGridView1.Rows.Add(datos[0], datos[1], datos[2], datos[3], datos[4], datos[5]);
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
            BuscarProducto();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

        private void Cliente_Click(object sender, EventArgs e)
        {

        }
    }
}
