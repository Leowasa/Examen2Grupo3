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
    public partial class Buscar_Usuarios
    {
        private Button btnBuscarUsuario;

        public Buscar_Usuarios()
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
    }
}
