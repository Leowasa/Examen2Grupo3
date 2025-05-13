
using System.Text.Json;
using System.util;
using Examen2Grupo3;
using System.Data;
using static Examen2Grupo3.Datos;
using Examen2Grupo3.Properties;
using ejemplo;
using System.Collections.Immutable;
using System.Windows.Forms;
using ejemplo;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ejemplo
{
    public partial class Usuarios : Form
    {

        private Panel PanelPrincipal = new Panel();
        Datos.Usuarios Usuarioactual = new Datos.Usuarios();
        Datos.Usuarios Nuevo = new Datos.Usuarios();
        List<Datos.Usuarios> usuarios = new List<Datos.Usuarios>();
        AgregarCliente Operar;
        public Usuarios(Datos.Usuarios usuarioactual)
        {

            InitializeComponent(); 
            usuarios = LeerUsuarios(); // Carga la lista de usuarios

            Operar = new AgregarCliente(1, usuarios); // Inicializa el formulario de agregar cliente
            this.Usuarioactual = usuarioactual;//cargo el usuario actual
            ControlUsuario1(usuarioactual);//y aplico las restricciones
            CargarDatosEnDataGridView();

        }

        public void AbrirFormulario(Form formulario)
        {
            // Limpiar el panel antes de agregar el nuevo formulario
            PanelPrincipal.Controls.Clear();

            // Configurar el formulario dentro del panel
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            // Agregar el formulario al panel y mostrarlo
            PanelPrincipal.Controls.Add(formulario);
            formulario.Show();
        }


        private List<Datos.Usuarios> LeerUsuarios()
        {
            // Obtener la ruta de la carpeta "Data" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Usuario");
            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, "usuarios.json");


            // Verificar si el archivo existe
            if (!File.Exists(rutaCompleta))
            {
                return new List<Datos.Usuarios>(); // Retornar una lista vacía si no existe
            }

            // Leer el contenido del archivo JSON
            string json = File.ReadAllText(rutaCompleta);

            // Deserializar el contenido JSON en una lista de usuarios
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Datos.Usuarios>>(json) ?? new List<Datos.Usuarios>();
        }

        public void ControlUsuario1(Datos.Usuarios Usuarioactual)
        {

            if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }

        }



        private void Usuarios_Load(object sender, EventArgs e)
        {

        }

        private void CargarDatosEnDataGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (var row in usuarios)
            {
                if (row == null) continue; // Verificar si la fila es nula
                dataGridView1.Rows.Add(row.ID, row.Nombre, row.Username, row.Tipo);//muestro la lista de usuarios en el datagrid

            }

        }
        public void GuardarUsuarios(string rutaArchivo)
        {
            // Obtener la ruta de la carpeta "Usuario" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Usuario");

            // Crear la carpeta "Usaurio" si no existe
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, rutaArchivo);

            // Guardar el archivo JSON en la ruta especificada
            string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(rutaCompleta, json);
        }

        public void ImportarCSV(string rutaArchivo)
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    var lineas = File.ReadAllLines(rutaArchivo);
                    usuarios.Clear();

                    foreach (var linea in lineas.Skip(1)) // Omitir encabezado
                    {
                        var datos = linea.Split(','); // Dividir la línea en columnas

                        // Verificar que la línea tenga suficientes columnas
                        if (datos.Length > 3)
                        {
                            // Crear un nuevo objeto Usuarios y asignar los valores
                            var usuario = new Datos.Usuarios
                            {
                                ID = int.Parse(datos[0]), // Convertir el ID a entero
                                Nombre = datos[1],
                                Username = datos[2],
                                Password = datos[3],
                                Tipo = datos[4]
                            };

                            // Agregar el usuario a la lista
                            usuarios.Add(usuario);

                        }
                    }
                    GuardarUsuarios("usuarios.Json");
                    CargarDatosEnDataGridView();
                    MessageBox.Show("Importación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else MessageBox.Show("Importación  no completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    sw.WriteLine("ID,Nombre,Username,Password,Tipo"); // Encabezado CSV  

                    foreach (var fila in usuarios)
                    {

                        sw.WriteLine($"{fila.ID},{fila.Nombre},{fila.Username},{fila.Password},{fila.Tipo}");

                    }
                }

                MessageBox.Show("Exportación realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)//btn para exportar archivo csv
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

        private void pictureBox2_Click(object sender, EventArgs e)//btn para importar archivo csv
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos CSV (*.csv)|*.csv";
                ofd.Title = "Selecciona un archivo CSV para importar";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ImportarCSV(ofd.FileName);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al importar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
     
        private void dataGridView1_CellClick_2(object sender, DataGridViewCellEventArgs e)
        {


            if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {


                if (Usuarioactual.Tipo == "Aprobador" || (Usuarioactual.Tipo == "Registrador"))
                {
                    MessageBox.Show("Necesita Ser Administrador para realizar esta operacion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (usuarios[e.RowIndex].Tipo == "Administrador" || usuarios[e.RowIndex].Tipo == "SuperUsuario")
                {
                    MessageBox.Show("No tiene permisos para eliminar este tipo de Usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                DialogResult result = MessageBox.Show($"¿Deseas eliminar a {usuarios[e.RowIndex].Username}?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    usuarios.RemoveAt(e.RowIndex);
                    GuardarUsuarios("usuarios.json");
                    CargarDatosEnDataGridView();
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                if (Usuarioactual.Tipo == "Aprobador" || (Usuarioactual.Tipo == "Registrador"))
                {
                    MessageBox.Show("Necesita Ser Administrador para realizar esta operacion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (usuarios[e.RowIndex].Tipo == "Administrador" || usuarios[e.RowIndex].Tipo == "SuperUsuario")
                {
                    MessageBox.Show("No tiene permisos para realizar esta operacion. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];
                // Verificar que los valores requeridos no sean nulos  
                if (usuarios[e.RowIndex] != null)
                {
                    // Crear una instancia del formulario de edición y pasar los datos  
                    Operar = new AgregarCliente(3, usuarios);
                    Operar.SetDatosUsuarios(usuarios[e.RowIndex]);
                    Operar.IdOriginal = usuarios[e.RowIndex].ID;
                    // Mostrar el formulario de edición como una ventana modal  
                    if (Operar.ShowDialog() == DialogResult.OK)
                    {
                        usuarios[e.RowIndex] = Operar.DatosUsuario;
                        GuardarUsuarios("usuarios.json");
                        CargarDatosEnDataGridView();
                    }
                }
                else
                {
                    MessageBox.Show("Algunos datos requeridos están vacíos o son nulos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 3 caracteres
            if (textoBusqueda.Length < 3)
            {
                // Si tiene menos de 3 caracteres, mostrar todas las filas
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)//barra de busqueda
        {
            BuscarElemento(guna2TextBox1.Text);
        }


        private void btnAgregar_Click(object sender, EventArgs e)//btn agregar usuario
        {
            if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")//solo el Admin puede agregar usuarios
            {
                MessageBox.Show("Necesita Ser Administrador para realizar esta operacion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            Operar = new AgregarCliente(1, usuarios);
            if (Operar.ShowDialog() == DialogResult.OK)//llamo al formulario para crear un nuevo usuario. si el resultado es ok se ejecuta el cuerpo
            {
                usuarios.Add(Operar.DatosUsuario);
                GuardarUsuarios("usuarios.json");

                CargarDatosEnDataGridView();

            }
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

           
        }
    }
}
