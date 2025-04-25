using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
                Bitmap img = new Bitmap(Application.StartupPath + @"\img\Fondo.jpg");
                formAgregar.BackgroundImage = img;
                formAgregar.BackgroundImageLayout = ImageLayout.Stretch;
                formAgregar.Text = "Añadir Usuario";
                formAgregar.Size = new Size(300, 350);


                var lblId = new Label { Text = "ID:", Location = new Point(10, 20), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtId = new TextBox { Location = new Point(110, 20), Width = 150 };

                var lblNombre = new Label { Text = "Nombre:", Location = new Point(10, 60), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtNombre = new TextBox { Location = new Point(110, 60), Width = 150 };

                var lblUsername = new Label { Text = "Username:", Location = new Point(10, 100), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtUsername = new TextBox { Location = new Point(110, 100), Width = 150 };

                var lblPassword = new Label { Text = "Contraseña:", Location = new Point(10, 140), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtPassword = new TextBox { Location = new Point(110, 140), Width = 150, PasswordChar = '*' };

                var lblTipo = new Label { Text = "Tipo de Usuario:", Location = new Point(10, 180), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var cmbTipo = new ComboBox { Location = new Point(110, 180), Width = 150 };
                cmbTipo.Items.AddRange(new[] { "Administrador", "Usuario", "Registrador", "Aprobador" });

                if (existeAdministrador)
                {
                    cmbTipo.Items.Remove("Administrador");
                }

                var btnGuardarUsuario = new Button { Text = "Guardar", Location = new Point(100, 220), Width = 80 };
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

        private void BtnBuscarUsuario_Click(object sender, EventArgs e)
        {
            using (var formBuscar = new Form())
            {
                formBuscar.Text = "Buscar Usuario";
                formBuscar.Size = new Size(600, 400);

                Bitmap img = new Bitmap(Application.StartupPath + @"\img\fondo.jpg");
                formBuscar.BackgroundImage = img;
                formBuscar.BackgroundImageLayout = ImageLayout.Stretch;

                var lblCriterio = new Label { Text = "Buscar por (ID o Nombre):", Location = new Point(10, 20), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
                var txtCriterio = new TextBox { Location = new Point(170, 20), Width = 300 };

                var lstSugerencias = new ListBox { Location = new Point(50, 60), Width = 500, Height = 200, Visible = false };

                var btnBuscarUsuario = new Button { Text = "Buscar", Location = new Point(250, 280), Width = 80 };
                btnBuscarUsuario.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtCriterio.Text))
                    {
                        MessageBox.Show("Por favor, ingrese un criterio de búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var usuarios = LeerUsuarios();
                    var usuario = usuarios.FirstOrDefault(u => u.Id == txtCriterio.Text || u.Nombre.Equals(txtCriterio.Text, StringComparison.OrdinalIgnoreCase));

                    if (usuario == null)
                    {
                        MessageBox.Show("Usuario no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Usuario encontrado:\n\nNombre: {usuario.Nombre}\nUsername: {usuario.Username}\nTipo: {usuario.Tipo}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };

                txtCriterio.TextChanged += (s, args) =>
                {
                    if (txtCriterio.Text.Length >= 4)
                    {
                        var usuarios = LeerUsuarios();
                        var sugerencias = usuarios
                            .Where(u => u.Id.Contains(txtCriterio.Text) || u.Nombre.IndexOf(txtCriterio.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                            .Select(u => $"{u.Id} - {u.Nombre} ({u.Tipo})")
                            .ToList();

                        lstSugerencias.Items.Clear();
                        lstSugerencias.Items.AddRange(sugerencias.ToArray());
                        lstSugerencias.Visible = sugerencias.Any();
                    }
                    else
                    {
                        lstSugerencias.Visible = false;
                    }
                };

                lstSugerencias.DoubleClick += (s, args) =>
                {
                    if (lstSugerencias.SelectedItem != null)
                    {
                        txtCriterio.Text = lstSugerencias.SelectedItem.ToString().Split('-')[0].Trim();
                        lstSugerencias.Visible = false;
                    }
                };

                formBuscar.Controls.Add(lblCriterio);
                formBuscar.Controls.Add(txtCriterio);
                formBuscar.Controls.Add(lstSugerencias);
                formBuscar.Controls.Add(btnBuscarUsuario);

                formBuscar.ShowDialog();
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



