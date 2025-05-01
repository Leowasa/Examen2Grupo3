
using Examen2Grupo3;

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var usuarios = LeerUsuarios();

            if (textBox1.Text.Length >= 4)
            {
                var sugerencias = usuarios
                    .Where(u => u.Id.Contains(textBox1.Text) || u.Nombre.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0)
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

        public class Usuario
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public string Username { get; set; }
            public string Tipo { get; set; }
        }

        private List<Usuario> LeerUsuarios()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Usuario>();
            }

            string json = File.ReadAllText(FilePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(json) ?? new List<Usuario>();
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Location = new Point(45, 170);
            dataGridView1.Size = new Size(1000, 400);
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
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
        }
    }
}

