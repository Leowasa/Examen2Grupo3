using Newtonsoft.Json;
namespace Examen2Grupo3;
using ejemplo;
using System;

public partial class Login : Form
{
    List<RegistroPedidos.Usuarios> listaUsuarios = new List<RegistroPedidos.Usuarios>();

    public RegistroPedidos.Usuarios usuario = new RegistroPedidos.Usuarios();

    public Login()
    {
        InitializeComponent();
    }
    private void guna2Button2_Click(object sender, EventArgs e)
    {
        cargarCargarUsuario();

        // Validar si los campos están vacíos
        if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox3.Text))
        {
            MessageBox.Show("Por favor, complete todos los campos antes de iniciar sesión.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        try
        {
            usuario.Username = guna2TextBox1.Text;
            usuario.Password = guna2TextBox3.Text;

            if (listaUsuarios != null)
            {
                bool usuarioEncontrado = false;
                foreach (var lista in listaUsuarios)
                {
                    if (lista.Username == usuario.Username && lista.Password == usuario.Password)
                    {

                        usuarioEncontrado = true;
                        personalizar(lista);
                        break;
                    }
                }

                if (!usuarioEncontrado)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error al cargar los usuarios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void cargarCargarUsuario()
    {
        string rutarchivo = "usuarios.json";
        if (File.Exists(rutarchivo))
        {
            string usuarios = File.ReadAllText(rutarchivo);
            try
            {
                listaUsuarios = JsonConvert.DeserializeObject<List<RegistroPedidos.Usuarios>>(usuarios);
            }
            catch
            {
                MessageBox.Show("error");
            }

        }

    }
    public void personalizar(RegistroPedidos.Usuarios usuarios)
    {
        switch (usuarios.Tipo)
        {
            case "Administrador":
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();
                break;
            case "Aprobador":
                Form1 form2 = new Form1();
                form2.guna2Button12.Visible = false;
                this.Hide();
                // Assuming "Mibotn" is a control or property, it must be accessed correctly.
                // Replace "Mibotn" with the correct property or method name.
                form2.Show();
                break;
            case "Registrador":
                Form1 form3 = new Form1();
                form3.guna2Button11.Visible = false;
                this.Hide();
                form3.Show();
                break;
        }
    }
}