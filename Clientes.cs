using Examen2Grupo3;
using Newtonsoft.Json;
using System.Text.Json;
using static Examen2Grupo3.Datos;

namespace ejemplo
{
    public partial class Clientes : Form
    {

        public List<Datos.Cliente> cliente = new List<Cliente>();
        public List<Datos.Cliente> ListaOriginal = new List<Cliente>();//ListaOriginal
        private BindingSource bindingSource = new BindingSource();
        public Datos.Usuarios usuarioActual = new Datos.Usuarios();
        public Clientes(Datos.Usuarios UsuarioActual)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
            CargarClientes("Clientes.Json");
            this.usuarioActual = UsuarioActual;
            ControlUsuario1(usuarioActual);
            tootip();

        }
        private void tootip()
        {
            ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
            toolTip1.SetToolTip(pictureBox1, "Importar.");
            ToolTip tooltip2 = new ToolTip();
            tooltip2.SetToolTip(pictureBox2, "Exportar.");

        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)//barra buscar cliente
        {
            BuscarElemento(guna2TextBox2.Text);
        }
        private void BuscarElemento(string textoBusqueda)
        {
            if ((string.IsNullOrWhiteSpace(textoBusqueda) || textoBusqueda.Length < 3))
            {
                // Mostrar todos los productos
                bindingSource.DataSource = new List<Cliente>(ListaOriginal);

            }
            else
            {
                string filtro = textoBusqueda.ToLower();
                var filtrados = ListaOriginal.Where(p =>
                    p.ID.ToString().Contains(filtro) ||
                    (p.Nombre != null && p.Nombre.ToLower().Contains(filtro))
                ).ToList();

                bindingSource.DataSource = filtrados;
            }
            bindingSource.ResetBindings(false);
        }
        private void guna2Button2_Click(object sender, EventArgs e)//btn agregar cliente
        {
            AgregarCliente agregarCliente = new AgregarCliente(2, cliente);
            if (agregarCliente.ShowDialog() == DialogResult.OK)
            {

                cliente.Add(agregarCliente.DatosClientes);
                GuardarClientes("Clientes.Json");
                CargarClientes("Clientes.Json");



            }
        }
        public void ImportarCSV(string rutaArchivo)
        {
            cliente = new List<Cliente>();

            try
            {
                if (File.Exists(rutaArchivo))
                {
                    var lineas = File.ReadAllLines(rutaArchivo);

                    foreach (var linea in lineas.Skip(1)) // Omitimos el encabezado
                    {
                        var datos = linea.Split(',');


                        Cliente clientes = new Cliente
                        {
                            ID = int.Parse(datos[0]),
                            Nombre = datos[1],
                        
                            Direccion = datos[2],
                            Tipo = datos[3]
                        };

                        cliente.Add(clientes);
                    }

                    MessageBox.Show("Importación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GuardarClientes("Clientes.Json");
                    CargarClientes("Clientes.Json");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(cliente, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(rutaArchivo, json);
                CargarClientes("Clientes.Json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista del cliente.", "Error de operacion", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }

        }
        public void CargarClientes(string rutaArchivo)
        {
            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                cliente = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(json); // Cambiar 'Cliente' a 'cliente'  

                ListaOriginal = new List<Cliente>(cliente); // Copia original

                bindingSource.DataSource = cliente;
                bindingSource.ResetBindings(false);
            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//casilla seleccionadad del datagrivew
        {
            Codigo_especial Form = new Codigo_especial();
            if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index && e.RowIndex >= 0)
            {

                if (usuarioActual.Tipo == "Aprobador" || usuarioActual.Tipo == "Registrador")//si el usuario es aprobador o registrador, solicitar el codigo especial
                {
                    Form.ShowDialog();
                    if (Form.DialogResult == DialogResult.OK)
                    {
                        //proceder con el resto del codigo

                    }
                    else return;

                }



                // Verificar que los valores requeridos no sean nulos  
                if (cliente[e.RowIndex] != null)
                {
                    // Crear una instancia del formulario de edición y pasar los datos  
                    AgregarCliente formEditar = new AgregarCliente(4, cliente);
                    formEditar.SetDatosCiente(cliente[e.RowIndex]);
                    formEditar.IdOriginal = cliente[e.RowIndex].ID;

                    formEditar.ShowDialog(); // Mostrar el formulario de edición como una ventana modal  
                    if (formEditar.DialogResult == DialogResult.OK)
                    {
                        cliente[e.RowIndex] = formEditar.DatosClientes;
                        GuardarClientes("Clientes.Json");
                        CargarClientes("Clientes.Json");
                    }
                }
                else
                {
                    MessageBox.Show("Algunos datos requeridos están vacíos o son nulos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                if (usuarioActual.Tipo == "Aprobador" || usuarioActual.Tipo == "Registrador")//si el usuario es aprobador o registrador, solicitar el codigo especial
                {
                    Form.ShowDialog();
                    if (Form.DialogResult == DialogResult.OK)
                    {
                        //proceder con el resto del codigo
                    }
                    else return;
                }


                DialogResult result = MessageBox.Show("¿Deseas eliminar a este cliente?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    cliente.RemoveAt(e.RowIndex); // Eliminar la fila seleccionada  
                    GuardarClientes("Clientes.Json");
                    CargarClientes("Clientes.Json");
                }
            }



        }

        private void pictureBox1_Click(object sender, EventArgs e)//btn para importar csv
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

        private void pictureBox2_Click(object sender, EventArgs e)//btn para exportar csv
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
        public void ControlUsuario1(Datos.Usuarios Usuarioactual)//control de usuarios para ocultar funciones al usuario
        {

            if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }

        }

        public void CargarUsuarios(List<Datos.Usuarios> Lista)
        {
            string rutarchivo = "usuarios.json";
            if (File.Exists(rutarchivo))
            {
                string usuarios = File.ReadAllText(rutarchivo);
                try
                {
                    Lista = JsonConvert.DeserializeObject<List<Datos.Usuarios>>(usuarios) ?? new List<Datos.Usuarios>();
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
