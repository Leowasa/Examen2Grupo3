
using System.Text.Json;
using System.util;
using Examen2Grupo3;
using System.Data;
using static Examen2Grupo3.RegistroPedidos;
using Examen2Grupo3.Properties;
using ejemplo;
using System.Collections.Immutable;
using System.Windows.Forms;

namespace ejemplo
{
    public partial class Usuarios : Form
    {

        private Panel PanelPrincipal;
        RegistroPedidos.Usuarios Usuarioactual = new RegistroPedidos.Usuarios();
        List<RegistroPedidos.Usuarios> usuarios = new List<RegistroPedidos.Usuarios>();
        public Usuarios(RegistroPedidos.Usuarios usuarioactual)
        {

            InitializeComponent(); // Inicializa los controles del formulario
            dataGridView1.Visible = true;
            usuarios = LeerUsuarios(); // Carga la lista de usuarios
           

            this.Usuarioactual = usuarioactual;
            ControlUsuario1(usuarioactual);
            ConfigurarTextBox();

        }

        private void AbrirFormulario(object? formhija)
        {
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
            usuarios = LeerUsuarios();
            dataGridView1.Rows.Clear();
            CargarDatosEnDataGridView();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string textoBusqueda = textBox1.Text.Trim(); // Obtener el texto de búsqueda y eliminar espacios en blanco
            var usuarios = LeerUsuarios();
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
            CargarDatosEnDataGridView(); // Llena el DataGridView con los datos
        }

        private void CargarDatosEnDataGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (var row in usuarios)
            {
          
                dataGridView1.Rows.Add(row.ID, row.Nombre, row.Username, row.Tipo);

            }

        }
        public void GuardarUsuarios(string rutaArchivo)
        {

            string json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
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
                        //     dataGridView1.DataSource = null;
                        //     dataGridView1.DataSource = usuariosExistentes;

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {

                if (Usuarioactual.Tipo == "Aprobador" || (Usuarioactual.Tipo == "Registrador"))
                {
                    MessageBox.Show("Necesita Ser Administrador para realizar la operacion.");
                    return;
                }


                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {

                    usuarios.RemoveAt(e.RowIndex);
                    dataGridView1.Rows.Clear();
                    GuardarUsuarios("usuarios.json");
                    CargarDatosEnDataGridView();
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index && e.RowIndex >= 0) 
            {
                AbrirFormulario(new Agregar_Usuarios());

            }
        }
    }
}
/*     private void ConfigurarDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Location = new Point(12, 165);
            dataGridView1.Size = new Size(1070, 800);
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            // Ocultar la columna de contraseña si existe
            if (dataGridView1.Columns["Password"] != null)
            {
                dataGridView1.Columns["Password"].Visible = false;
            }
        }*/





// var sugerencias = usuarios
//  .Where(u => u.ID.ToString().Contains(textBox1.Text, StringComparison.OrdinalIgnoreCase) ||
//             u.Nombre.Contains(textBox1.Text, StringComparison.OrdinalIgnoreCase))
// .ToList();