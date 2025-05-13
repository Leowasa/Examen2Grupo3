using Examen2Grupo3;
using Newtonsoft.Json;
using System.Text.Json;
using System.Windows.Forms;
using static Examen2Grupo3.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ejemplo
{
    public partial class Clientes : Form
    {
       
        public List<Datos.Cliente> cliente = new List<Cliente>();
        public Datos.Usuarios usuarioActual = new Datos.Usuarios();
        public Clientes(Datos.Usuarios UsuarioActual)
        {
            InitializeComponent();
            CargarClientes("Clientes.Json");
            this.usuarioActual = UsuarioActual;
            ControlUsuario1(usuarioActual);
           

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)//barra buscar cliente
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
                            Correo = datos[2],
                            Direccion = datos[3],
                            Tipo = datos[4]
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
            } catch (Exception ex)
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
                cliente = System.Text.Json.JsonSerializer.Deserialize<List<Datos.Cliente>>(json); // Cambiar 'Cliente' a 'cliente'  

                if (cliente != null) // Verificar que la lista no sea nula  
                {
                    dataGridView1.Rows.Clear(); // Limpiar la tabla antes de cargar nuevos datos  

                    foreach (var producto in cliente)
                    {
                        dataGridView1.Rows.Add(producto.ID, producto.Nombre, producto.Correo, producto.Direccion, producto.Tipo);
                    }
                }
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
       
        private void pictureBox1_Click(object sender, EventArgs e)//btn para exportar csv
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

        private void pictureBox2_Click(object sender, EventArgs e)//btn para importart csv
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
                    Lista = JsonConvert.DeserializeObject<List<Datos.Usuarios>>(usuarios)?? new List<Datos.Usuarios>();
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
