
using Examen2Grupo3;

namespace ejemplo
{
    public partial class Usuarios : Form
    {
        private Panel PanelPrincipal;

        public Usuarios()
        {
            InitializeComponent();
            PanelPrincipal = new Panel
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(PanelPrincipal);

            if (this.PanelPrincipal.Controls.Count > 0)
                this.PanelPrincipal.Controls.RemoveAt(0);

            ConfigurarDataGridView();
            ConfigurarDataGridViewDecorativa();
            ConfigurarTextBox();
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
                // Mostrar los últimos 3 usuarios añadidos si no hay coincidencias  
                var ultimosUsuarios = usuarios.OrderByDescending(u => usuarios.IndexOf(u)).Take(3).ToList();
                dataGridView1.DataSource = ultimosUsuarios;
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
            public string Password { get; set; }
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
            dataGridView1.Location = new Point(45, 360); // Mover el DataGridView a la posición (0, 40)  
            dataGridView1.Size = new Size(989, 400);
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
        private void ConfigurarDataGridViewDecorativa()
        {
            DataGridView dataGridViewDecorativa = new DataGridView
            {
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Location = new Point(45, 760), // Posición debajo del DataGridView principal
                Size = new Size(989, 400),
                ScrollBars = ScrollBars.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Enabled = false // Deshabilitar interacción
            };

            // Opcional: Estilo decorativo
            dataGridViewDecorativa.DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewDecorativa.DefaultCellStyle.ForeColor = Color.DarkGray;
            dataGridViewDecorativa.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridViewDecorativa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
    }
}

