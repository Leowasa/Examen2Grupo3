using Examen2Grupo3;
using Newtonsoft.Json;
using System.Text.Json;
using System.Windows.Forms;
using static Examen2Grupo3.RegistroPedidos;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ejemplo
{
    public partial class Clientes : Form
    {
        //  public List<RegistroPedidos.Usuarios> Usuarioss = new List<RegistroPedidos.Usuarios>();
        public List<Cliente>? cliente = new List<Cliente>();
        public RegistroPedidos.Usuarios usuarioActual = new RegistroPedidos.Usuarios();
        BindingSource bindingSource = new BindingSource();
        public Clientes(RegistroPedidos.Usuarios UsuarioActual)
        {
            InitializeComponent();
            CargarClientes("Clientes.Json");
            this.usuarioActual = UsuarioActual;
            ControlUsuario1(usuarioActual);
           

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
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
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AgregarCliente agregarCliente = new AgregarCliente();
            if (agregarCliente.ShowDialog() == DialogResult.OK)
            {


                Cliente Clientenuevo = new Cliente()
                {
                    ID = agregarCliente.Datos.ID,
                    Nombre = agregarCliente.Datos.Nombre,
                    Direccion = agregarCliente.Datos.Direccion,
                    Correo = agregarCliente.Datos.Correo,
                    Tipo = agregarCliente.Datos.Tipo
                };

                cliente.Add(Clientenuevo);
                dataGridView1.Rows.Add(Clientenuevo.ID, Clientenuevo.Nombre, Clientenuevo.Direccion, Clientenuevo.Correo, Clientenuevo.Tipo);
                GuardarClientes("Clientes.Json");



            }
        }
        public void ExportarCSV(string rutaArchivo)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaArchivo))
                {
                    sw.WriteLine("ID,Nombre,CorreoElectronico,Direccion,Tipo"); // Encabezado CSV

                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        if (fila.Cells["ID"].Value != null)
                        {
                            sw.WriteLine($"{fila.Cells["ID"].Value},{fila.Cells["Nombre"].Value},{fila.Cells["CorreoElectronico"].Value},{fila.Cells["Direccion"].Value},{fila.Cells["Tipo"].Value}");
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
        public void GuardarClientes(string rutaArchivo)
        {
            cliente = new List<Cliente>();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["ID"].Value != null) // Validamos que la fila tenga datos
                {
                    Cliente clientes = new Cliente();
                    try
                    {
                        clientes.ID = int.Parse(fila.Cells["ID"].Value.ToString());
                        clientes.Nombre = fila.Cells["Nombre"].Value.ToString();
                        clientes.Direccion = fila.Cells["Direccion"].Value.ToString();
                        clientes.Correo = fila.Cells["CorreoElectronico"].Value.ToString();
                        clientes.Tipo = fila.Cells["Tipo"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                    cliente.Add(clientes); // Agregamos al inicio para mantener el orden
                }
            }
            string json = System.Text.Json.JsonSerializer.Serialize(cliente, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaArchivo, json);
            CargarClientes("Clientes.Json");
        }
        public void CargarClientes(string rutaArchivo)
        {

            if (File.Exists(rutaArchivo))
            {

                string json = File.ReadAllText(rutaArchivo);
                List<Cliente>? listaProductos = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(json);

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

                        dataGridView1.Rows.Add(datos[0], datos[1], datos[2], datos[3], datos[4]);
                    }

                    MessageBox.Show("Importación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GuardarClientes("Clientes.Json");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Clientes_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Codigo_especial Form = new Codigo_especial();
            if (usuarioActual.Tipo == "Aprobador" || usuarioActual.Tipo == "Registrador")
            {
                Form.ShowDialog();
                if (Form.DialogResult == DialogResult.OK)
                {
                    editar(e);
                    return;
                }
            }
            editar(e);




        }
        public void editar(DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                // Creamos una instancia del formulario de edición y pasamos los datos
                AgregarCliente formEditar = new AgregarCliente();
                formEditar.SetDatosProducto(
                    filaSeleccionada.Cells["ID"].Value.ToString(),
                    filaSeleccionada.Cells["Nombre"].Value.ToString(),
                    filaSeleccionada.Cells["Direccion"].Value.ToString(),
                    filaSeleccionada.Cells["CorreoElectronico"].Value.ToString(),
                    filaSeleccionada.Cells["Tipo"].Value.ToString()
                );

                formEditar.ShowDialog(); // Mostramos el formulario de edición como una ventana modal
                if (formEditar.DialogResult == DialogResult.OK)
                {
                    DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                    Cliente productoEditado = formEditar.ObtenerClienteEditado();
                    fila.Cells["ID"].Value = productoEditado.ID;
                    fila.Cells["Nombre"].Value = productoEditado.Nombre;
                    fila.Cells["Direccion"].Value = productoEditado.Direccion;
                    fila.Cells["CorreoElectronico"].Value = productoEditado.Correo;
                   
                    fila.Cells["Tipo"].Value = productoEditado.Tipo;
                    GuardarClientes("Clientes.Json");
                    CargarClientes("Clientes.Json");
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
                    GuardarClientes("Clientes.Json");
                }



            }
        }

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

        public void CargarUsuarios(List<RegistroPedidos.Usuarios> Lista)
        {
            string rutarchivo = "usuarios.json";
            if (File.Exists(rutarchivo))
            {
                string usuarios = File.ReadAllText(rutarchivo);
                try
                {
                    Lista = JsonConvert.DeserializeObject<List<RegistroPedidos.Usuarios>>(usuarios);
                }
                catch
                {
                    MessageBox.Show("error");
                }

            }

        }


        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {


        }

    }
}
