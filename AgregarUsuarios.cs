using System.Data;

namespace Examen2Grupo3

{
    public partial class Agregar_Usuarios
    {
        private Button btnAgregarUsuario;

        public Agregar_Usuarios()
        {
            // Crear el archivo JSON si no existe      
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]"); // Crear un archivo vacío con una lista JSON vacía      
            }
            var usuarios = LeerUsuarios();
            bool existeAdministrador = usuarios.Any(u => u.Tipo == "Administrador");

            using (var formAgregar = new Form())
            {
                Bitmap img = new Bitmap(Properties.Resources.Fondo);
           
                formAgregar.BackgroundImage = img;
                formAgregar.BackgroundImageLayout = ImageLayout.Stretch;
                formAgregar.Text = "Añadir Usuario";
                formAgregar.Size = new Size(300, 350);

                var lblId = new Label { Text = "ID:", Location = new Point(10, 20), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtId = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Location = new Point(110, 20),
                    Width = 150,
                    Height = 22,
                    BackColor = Color.Transparent,
                    BorderRadius = 6
                };

                var lblNombre = new Label { Text = "Nombre:", Location = new Point(10, 60), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtNombre = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Location = new Point(110, 60),
                    Width = 150,
                    Height = 22,
                    BackColor = Color.Transparent,
                    BorderRadius = 6
                };

                var lblUsername = new Label { Text = "Username:", Location = new Point(10, 100), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtUsername = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Location = new Point(110, 100),
                    Width = 150,
                    Height = 22,
                    BackColor = Color.Transparent,
                    BorderRadius = 6
                };

                var lblPassword = new Label { Text = "Contraseña:", Location = new Point(10, 140), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtPassword = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Location = new Point(110, 140),
                    Width = 150,
                    Height = 22,
                    BackColor = Color.Transparent,
                    BorderRadius = 6,
                    PasswordChar = '*'
                };

                var lblTipo = new Label { Text = "Tipo de Usuario:", Location = new Point(10, 180), AutoSize = false, ForeColor = Color.White, BackColor = Color.Transparent };
                var cmbTipo = new Guna.UI2.WinForms.Guna2ComboBox
                { 
                    Location = new Point(110, 180),
                    ItemHeight = 18,
                    Width = 150,
                    BackColor = Color.Transparent,
                    BorderRadius = 6
                };
                cmbTipo.Items.AddRange(new[] { "Registrador", "Aprobador" });


                var btnGuardarUsuario = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "Confirmar",
                    Location = new Point(100, 240),
                    Width = 90,
                    Height = 22,
                    FillColor = SystemColors.HotTrack,
                    BackColor = Color.Transparent,
                    BorderRadius = 6
                };
                btnGuardarUsuario.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) ||
                        string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
                        cmbTipo.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!txtId.Text.All(char.IsDigit))
                    {
                        MessageBox.Show("El ID solo debe contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (usuarios.Any(u => u.Id == txtId.Text))
                    {
                        MessageBox.Show("El ID ya existe. Por favor, ingrese un ID único.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var nuevoUsuario = new Usuario
                    {
                        Id = txtId.Text,
                        Nombre = txtNombre.Text,
                        Username = txtUsername.Text,
                        Password = txtPassword.Text,
                        Tipo = cmbTipo.SelectedItem.ToString()
                    };

                    usuarios.Add(nuevoUsuario);
                    GuardarUsuarios(usuarios);

                    MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formAgregar.Close();
                };

                formAgregar.Controls.Add(lblId);
                formAgregar.Controls.Add(txtId);
                formAgregar.Controls.Add(lblNombre);
                formAgregar.Controls.Add(txtNombre);
                formAgregar.Controls.Add(lblUsername);
                formAgregar.Controls.Add(txtUsername);
                formAgregar.Controls.Add(lblPassword);
                formAgregar.Controls.Add(txtPassword);
                formAgregar.Controls.Add(lblTipo);
                formAgregar.Controls.Add(cmbTipo);
                formAgregar.Controls.Add(btnGuardarUsuario);

                formAgregar.ShowDialog();
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

        private void GuardarUsuarios(List<Usuario> usuarios)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(usuarios, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}



