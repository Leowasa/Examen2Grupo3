using Examen2Grupo3;
using Newtonsoft.Json;

namespace ejemplo
{
    public partial class Usuarios : Form
    {

        private Panel PanelPrincipal = new Panel();
        Datos.Usuarios Usuarioactual = new Datos.Usuarios();
        List<Datos.Usuarios> usuarios = new List<Datos.Usuarios>();
        private List<Datos.Usuarios> usuariosoriginal = new List<Datos.Usuarios>();
        AgregarCliente Operar;
        private BindingSource bindingSource = new BindingSource();
        public Usuarios(Datos.Usuarios usuarioactual)
        {

            InitializeComponent();
            LeerUsuarios(); // Carga la lista de usuarios
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
            Operar = new AgregarCliente(1, usuarios); // Inicializa el formulario de agregar cliente
            this.Usuarioactual = usuarioactual;//cargo el usuario actual
            ControlUsuario1(usuarioactual);//y aplico las restricciones
            tootip();


        }
        private void tootip()
        {
            toolTip1 = new System.Windows.Forms.ToolTip();
            toolTip1.SetToolTip(pictureBox1, "Importar.");
            ToolTip tooltip2 = new ToolTip();
            tooltip2.SetToolTip(pictureBox2, "Exportar.");

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


        private void LeerUsuarios()
        {
            // Obtener la ruta de la carpeta "Data" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Usuario");
            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, "usuarios.json");


            // Verificar si el archivo existe
            if (!File.Exists(rutaCompleta))
            {
                usuarios = new List<Datos.Usuarios>(); // Retornar una lista vacía si no existe
                return;
            }

            // Leer el contenido del archivo JSON
            string json = File.ReadAllText(rutaCompleta);

            // Deserializar el contenido JSON en una lista de usuarios
            usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Datos.Usuarios>>(json) ?? new List<Datos.Usuarios>();
            usuariosoriginal = usuarios;
            bindingSource.DataSource = usuarios;
            bindingSource.ResetBindings(false);

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
                    LeerUsuarios();
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
        private void pictureBox1_Click(object sender, EventArgs e)//btn para  archivo importar csv
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

        private void pictureBox2_Click(object sender, EventArgs e)//btn para exportar archivo csv
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
                    LeerUsuarios();
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
                        LeerUsuarios();
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
            if ((string.IsNullOrWhiteSpace(textoBusqueda) || textoBusqueda.Length < 3))
            {
                // Mostrar todos los productos
                bindingSource.DataSource = new List<Datos.Usuarios>(usuarios);

            }
            else
            {
                string filtro = textoBusqueda.ToLower();
                var filtrados = usuarios.Where(p =>
                    p.ID.ToString().Contains(filtro) ||
                    (p.Nombre != null && p.Nombre.ToLower().Contains(filtro))
                ).ToList();

                bindingSource.DataSource = filtrados;
            }
            bindingSource.ResetBindings(false);
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
                LeerUsuarios();

            }
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}