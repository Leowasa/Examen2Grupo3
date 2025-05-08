
using System.Text.Json;
using Examen2Grupo3;
using static Examen2Grupo3.RegistroPedidos;

namespace ejemplo
{
    public partial class Usuarios : Form
    {
        private Panel PanelPrincipal;
        RegistroPedidos.Usuarios Usuarioactual = new RegistroPedidos.Usuarios();

        public Usuarios(RegistroPedidos.Usuarios usuarioactual)
        {
            InitializeComponent();

            this.Usuarioactual = usuarioactual;
            ControlUsuario1(usuarioactual);
            PanelPrincipal = new Panel
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(PanelPrincipal);

            if (this.PanelPrincipal.Controls.Count > 0)
                this.PanelPrincipal.Controls.RemoveAt(0);

            ConfigurarDataGridView();
            ConfigurarTextBox();

            Usuarioactual = usuarioactual;
        }

        private void AbrirFormulario(object? formhija)
        {
            if (this.PanelPrincipal.Controls.Count > 0)
                this.PanelPrincipal.Controls.RemoveAt(0);
            Form? fh = formhija as Form;
            if (fh != null)
            {
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                this.PanelPrincipal.Controls.Add(fh);
                this.PanelPrincipal.Tag = fh;
                fh.Show();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Agregar_Usuarios());
            CargarDatosEnDataGridView();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            var usuarios = LeerUsuarios();

            if (textBox1.Text.Length >= 4)
            {
                var sugerencias = usuarios
                    .Where(u => u.ID.ToString().Contains(textBox1.Text, StringComparison.OrdinalIgnoreCase) ||
                                u.Nombre.Contains(textBox1.Text, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                dataGridView1.DataSource = sugerencias;
            }
            else
            {
                // Mostrar toda la lista de usuarios si no hay coincidencias  
                dataGridView1.DataSource = usuarios;
            }
            
            
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Ingresar Nombre o ID")
            {
                textBox1.Text = string.Empty;
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Ingresar Nombre o ID";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var usuarios = LeerUsuarios();
                var usuarioSeleccionado = usuarios[e.RowIndex];

                MessageBox.Show($"Usuario seleccionado:\n\nNombre: {usuarioSeleccionado.Nombre}\nUsername: {usuarioSeleccionado.Username}\nTipo: {usuarioSeleccionado.Tipo}",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private const string FilePath = "usuarios.json";

        private List<RegistroPedidos.Usuarios> LeerUsuarios()
        {
            if (!File.Exists(FilePath))
            {
                return new List<RegistroPedidos.Usuarios>();

            }

            string json = File.ReadAllText(FilePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<RegistroPedidos.Usuarios>>(json) ?? new List<RegistroPedidos.Usuarios>();
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar columnas al tamaño del DataGridView
            dataGridView1.Dock = DockStyle.Fill; // Ocupa todo el espacio disponible en el Panel
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            // Configuración de estilo
            dataGridView1.BackgroundColor = Color.FromArgb(45, 66, 91);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.SteelBlue;
            dataGridView1.RowHeadersVisible = false;


            // Ocultar la columna de contraseña si existe
            if (dataGridView1.Columns.Contains("Password"))
            {
                dataGridView1.Columns["Password"].Visible = false;
            }
        }
        private void ConfigurarTextBox()
        {
            textBox1.Text = "Ingresar Nombre o ID";
            textBox1.ForeColor = Color.Gray;
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
        }
        public void ControlUsuario1(RegistroPedidos.Usuarios Usuarioactual)
        {

            if (Usuarioactual.Tipo == "Aprobador" || Usuarioactual.Tipo == "Registrador")
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                dataGridView1.Columns["Password"].Visible = false;
            }

        }



        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView();
        }

        private void CargarDatosEnDataGridView()
        {
            var usuarios = LeerUsuarios();
            dataGridView1.DataSource = usuarios;

            // Ocultar la columna de contraseña después de cargar los datos
            if (dataGridView1.Columns["Password"] != null)
            {
                dataGridView1.Columns["Password"].Visible = false;
            }
        }
        public void GuardarUsuarios(string rutaArchivo)
        {
            List<RegistroPedidos.Usuarios> listaUsuarios = new List<RegistroPedidos.Usuarios>();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["Id"].Value != null) // Validamos que la fila tenga datos
                {
                    RegistroPedidos.Usuarios usuarios = new RegistroPedidos.Usuarios();
                    try
                    {
                        // Fix for CS0019 and CS8604 in the problematic line
                        usuarios.ID = Convert.ToInt32(fila.Cells["Id"].Value ?? 0);
                        usuarios.Nombre = fila.Cells["Nombre"].Value.ToString() ?? "";
                        usuarios.Username = fila.Cells["Username"].Value.ToString() ?? "";
                        usuarios.Password = fila.Cells["Password"].Value?.ToString() ?? "";
                        usuarios.Tipo = fila.Cells["Tipo"].Value.ToString() ?? "";


                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show($"Error de formato: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                    listaUsuarios.Insert(0, usuarios); // Agregamos al inicio para mantener el orden
                }
            }

            string json = JsonSerializer.Serialize(listaUsuarios, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaArchivo, json);

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

                        // Asegurarse de que la columna de contraseña esté incluida  
                        if (datos.Length >= 5)
                        {
                            dataGridView1.Rows.Add(datos[0], datos[1], datos[2], datos[3], datos[4]);
                        }
                    }

                    MessageBox.Show("Importación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GuardarUsuarios("Usuarios.Json");
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
                    sw.WriteLine("ID,Nombre,Username,Password,Tipo"); // Encabezado CSV  

                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        if (fila.Cells["ID"].Value != null)
                        {
                            sw.WriteLine($"{fila.Cells["Id"].Value},{fila.Cells["Nombre"].Value},{fila.Cells["Username"].Value},{fila.Cells["Password"].Value},{fila.Cells["Tipo"].Value}");
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
                    try
                    {
                        var usuariosExistentes = LeerUsuarios(); // Leer usuarios actuales  
                        var nuevosUsuarios = LeerUsuariosDesdeCSV(ofd.FileName); // Leer usuarios desde el CSV  

                        // Combinar listas evitando duplicados por ID  
                        foreach (var nuevoUsuario in nuevosUsuarios)
                        {
                            if (!usuariosExistentes.Any(u => u.ID == nuevoUsuario.ID))
                            {
                                usuariosExistentes.Add(nuevoUsuario);
                            }
                        }

                        // Guardar la lista combinada en el archivo JSON  
                        GuardarUsuarios(FilePath);

                        // Actualizar el DataGridView  
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = usuariosExistentes;

                        MessageBox.Show("Usuarios importados y añadidos correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al importar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private List<RegistroPedidos.Usuarios> LeerUsuariosDesdeCSV(string rutaArchivo)
        {
            var usuarios = new List<RegistroPedidos.Usuarios>();

            var lineas = File.ReadAllLines(rutaArchivo);
            foreach (var linea in lineas.Skip(1)) // Omitir encabezado  
            {
                var datos = linea.Split(',');
                if (datos.Length >= 4)
                {

                    usuarios.Add(new RegistroPedidos.Usuarios
                    {
                        ID = int.Parse(datos[0]),
                        Nombre = datos[1],
                        Username = datos[2],
                        Password = datos[3],
                        Tipo = datos[4]
                    });
                }
            }

            return usuarios;
        }

       
    }
}

